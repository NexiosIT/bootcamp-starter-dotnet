#!/bin/bash
command -v jq >/dev/null || exit 0
cd "$(dirname "$0")/../.." || exit 0
FILE=$(jq -r '.tool_input.file_path')
command -v cygpath >/dev/null && FILE=$(cygpath -u "$FILE")
case "$FILE" in
  *.cs)
    FILE="$(cd "$(dirname "$FILE")" && pwd -P)/$(basename "$FILE")" || exit 0
    # dotnet format --include only matches paths relative to the solution folder
    dotnet format --include "${FILE#"$(pwd -P)/"}" ;;
esac
exit 0
