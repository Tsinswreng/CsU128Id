#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$SCRIPT_DIR"
PROJECT_PATH="$ROOT_DIR/proj/Tsinswreng.CsU128Id/Tsinswreng.CsU128Id.csproj"
ARTIFACT_DIR="$ROOT_DIR/artifacts"
PACKAGE_VERSION="0.0.1-alpha"
PACKAGE_ID="Tsinswreng.CsU128Id"

# Fill these before publishing, or provide them from environment variables.
NUGET_SOURCE="${NUGET_SOURCE:-https://api.nuget.org/v3/index.json}"
NUGET_API_KEY="${NUGET_API_KEY:-__FILL_ME__}"

if [[ "$NUGET_API_KEY" == "__FILL_ME__" ]]; then
	echo "NUGET_API_KEY is not set."
	echo "Export NUGET_API_KEY first, then rerun this script."
	exit 1
fi

rm -rf "$ARTIFACT_DIR"
mkdir -p "$ARTIFACT_DIR"

dotnet pack "$PROJECT_PATH" \
	-c Release \
	-o "$ARTIFACT_DIR" \
	-p:PackageVersion="$PACKAGE_VERSION"

PACKAGE_PATH="$ARTIFACT_DIR/$PACKAGE_ID.$PACKAGE_VERSION.nupkg"

if [[ ! -f "$PACKAGE_PATH" ]]; then
	echo "Package not found: $PACKAGE_PATH"
	exit 1
fi

dotnet nuget push "$PACKAGE_PATH" \
	--api-key "$NUGET_API_KEY" \
	--source "$NUGET_SOURCE" \
	--skip-duplicate

echo "Published: $PACKAGE_PATH"
