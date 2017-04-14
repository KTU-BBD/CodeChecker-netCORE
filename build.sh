#!/usr/bin/env bash
dotnet restore && dotnet restore Tests
cd CodeChecker.Tests
dotnet test
