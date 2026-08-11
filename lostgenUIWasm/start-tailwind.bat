@echo off
tasklist /v /fi "imagename eq node.exe" | findstr /i "tailwind" >nul
if errorlevel 1 (
    start "Tailwind Watch" cmd /k npm run tailwind:watch
)