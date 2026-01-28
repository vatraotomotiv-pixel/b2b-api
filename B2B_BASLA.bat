@echo off
chcp 65001 >nul
cls
echo ========================================
echo   B2B BACKEND BASLAT
echo ========================================
echo.

cd /d "%~dp0\B2B.API"

echo [1/4] MySQL Kontrolu...
tasklist | find /i "mysqld.exe" >nul
if %errorlevel% neq 0 (
    echo    [HATA] MySQL CALISMIYOR!
    echo    XAMPP Control Panel'den MySQL'i baslat!
    pause
    exit /b 1
)
echo    [OK] MySQL calisiyor

echo.
echo [2/4] Database Kontrolu...
C:\xampp\mysql\bin\mysql.exe -u root -e "USE b2b_db;" 2>nul
if %errorlevel% neq 0 (
    echo    [UYARI] Database yok, olusturuluyor...
    C:\xampp\mysql\bin\mysql.exe -u root -e "CREATE DATABASE IF NOT EXISTS b2b_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;" 2>nul
)
echo    [OK] Database hazir

echo.
echo [3/4] Build Yapiliyor...
dotnet build >nul 2>&1
if %errorlevel% neq 0 (
    echo    [HATA] Build basarisiz! Hatalari gormek icin: dotnet build
    pause
    exit /b 1
)
echo    [OK] Build basarili

echo.
echo [4/4] Backend Baslatiliyor...
echo.
echo ========================================
echo   BACKEND HATALARI BURADA GORUNECEK
echo ========================================
echo.
echo   Swagger: http://localhost:5000/swagger
echo   API: http://localhost:5000/api/products
echo   Durdurmak: Ctrl+C
echo.
echo ========================================
echo.

dotnet run

pause
