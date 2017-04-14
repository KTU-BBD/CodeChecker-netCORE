#!/usr/bin/env bash
export FrameworkPathOverride=/usr/lib/mono/4.5/
dotnet restore && dotnet restore Tests
cd Tests
dotnet xunit
