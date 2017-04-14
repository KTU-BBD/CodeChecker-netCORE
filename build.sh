#!/usr/bin/env bash
dotnet restore && dotnet restore Tests
cd /usr/lib/mono/
ls
cd Tests

dotnet xunit
