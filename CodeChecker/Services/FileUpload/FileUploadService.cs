using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CodeChecker.Data;
using CodeChecker.Models.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CodeChecker.Services.FileUpload
{
    public class FileUploadService
    {
        private readonly IHostingEnvironment _environment;
        private readonly ApplicationDbContext _dbContext;

        public static readonly string[] AVAILABLE_IMAGE_MIMETYPES =
        {
            "image/png", "image/jpg", "image/jpeg"
        };

        public FileUploadService(IHostingEnvironment environment, ApplicationDbContext dbContext)
        {
            _environment = environment;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Saves and validates one given image as asset
        /// </summary>
        /// <param name="file">Uploaded image</param>
        /// <returns>Asset</returns>
        public async Task<Asset> SavePicture(IFormFile file)
        {
            return await SaveAsset(file, AVAILABLE_IMAGE_MIMETYPES);
        }

        /// <summary>
        /// Saves and validates given images as assets
        /// </summary>
        /// <param name="files">Uploaded images</param>
        /// <returns>Assets</returns>
        public async Task<ICollection<Asset>> SavePictures(ICollection<IFormFile> files)
        {
            return await SaveAssets(files, AVAILABLE_IMAGE_MIMETYPES);
        }

        private async Task<Asset> SaveAsset(IFormFile file, string[] availableMimeType)
        {
            if (availableMimeType.Contains(file.ContentType))
            {
                ICollection<IFormFile> assets = new Collection<IFormFile>();
                assets.Add(file);
                var result = await SaveAssets(assets, availableMimeType);
                return result.First();
            }

            return null;
        }

        private async Task<ICollection<Asset>> SaveAssets(ICollection<IFormFile> files, string[] availableMimeType)
        {
            foreach (var file in files)
            {
                if (!availableMimeType.Contains(file.ContentType))
                {
                    return null;
                }
            }

            var uploadDir = Path.Combine(_environment.WebRootPath, "assets");

            ICollection<Asset> assets = new Collection<Asset>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var assetName = GetRandomName() + Path.GetExtension(file.FileName);
                    Asset asset = new Asset
                    {
                        Name = assetName,
                        OriginalName = file.FileName,
                        Mimetype = file.ContentType
                    };
                    using (var fileStream = new FileStream(Path.Combine(uploadDir, assetName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    _dbContext.Assets.Add(asset);
                    assets.Add(asset);
                }
            }

            _dbContext.SaveChanges();

            return assets;
        }

        private string GetRandomName()
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(DateTime.Now.Millisecond.ToString());
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}