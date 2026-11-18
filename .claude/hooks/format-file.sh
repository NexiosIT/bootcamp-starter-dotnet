#!/bin/bash
cd "${CLAUDE_PROJECT_DIR:-.}" || exit 0
FILE=$(jq -r '.tool_input.file_path')
# dotnet format --include only matches paths relative to the solution folder
FILE=${FILE#"$PWD/"}
case "$FILE" in
  *.cs) dotnet format --include "$FILE" ;;
esac
