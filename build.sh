#!/usr/bin/env bash
dotnet restore && dotnet restore Tests
cd Tests
ls /usr/lib/mono
dotnet xunit
