#!/usr/bin/env bash
dotnet restore && dotnet restore Tests
cd /usr/lib/mono/
ls < cat
cd Tests

dotnet xunit
