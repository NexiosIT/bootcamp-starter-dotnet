#!/bin/bash
cd "$(dirname "$0")/../.." || exit 0
INPUT=$(cat)
printf '%s' "$INPUT" | grep -q '"stop_hook_active": *true' && exit 0
OUT=$(dotnet test --nologo 2>&1)
if [ $? -ne 0 ]; then
  echo "$OUT" | tail -30 >&2
  exit 2
fi
