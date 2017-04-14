#!/usr/bin/env bash
dotnet restore && dotnet restore Tests
dotnet test
