#!/usr/bin/env bash
dotnet restore && dotnet restore Tests
cd Tests
dotnet xunit
