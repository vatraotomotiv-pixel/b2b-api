@echo off
chcp 65001 >nul
cls
echo ========================================
echo   B2B PROJESI CALISTIR
echo ========================================
echo.

cd /d "%~dp0\B2B.API"

echo [1/2] Backend Baslatiliyor...
start "B2B Backend" cmd /k "dotnet run"
timeout /t 3 >nul

echo [2/2] Frontend Aciliyor...
cd /d "%~dp0\Frontend"
start "" "index.html"

echo.
echo ========================================
echo   BACKEND: http://localhost:5000
echo   FRONTEND: Tarayicida acilacak
echo ========================================
echo.
pause
