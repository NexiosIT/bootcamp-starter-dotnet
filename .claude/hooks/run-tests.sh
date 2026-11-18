#!/bin/bash
INPUT=$(cat)
[ "$(echo "$INPUT" | jq -r '.stop_hook_active')" = "true" ] && exit 0
cd "${CLAUDE_PROJECT_DIR:-.}" || exit 0
OUT=$(dotnet test --nologo 2>&1)
if [ $? -ne 0 ]; then
  echo "$OUT" | tail -30 >&2
  exit 2
fi
