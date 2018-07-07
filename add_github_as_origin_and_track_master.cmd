@echo OFF
for /f "delims=\" %%a in ("%cd%") do set dir=%%~nxa
@echo ON
git remote add origin https://github.com/snielsson/%dir%
git branch -u origin/master master
pause
