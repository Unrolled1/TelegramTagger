$projectPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$sln = Join-Path $projectPath "TelegramTags.sln"
$msbuild = "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"

$watcher = New-Object System.IO.FileSystemWatcher
$watcher.Path = $projectPath
$watcher.IncludeSubdirectories = $true
$watcher.EnableRaisingEvents = $true

$extensions = @(".cs", ".json", ".resx", ".csproj")

$lastBuild = [datetime]::MinValue

Register-ObjectEvent $watcher Changed -Action {
    $path = $Event.SourceEventArgs.FullPath
    $ext = [System.IO.Path]::GetExtension($path)

    if ($ext -notin $extensions) {
        return
    }

    if ((Get-Date) - $script:lastBuild -lt [TimeSpan]::FromSeconds(2)) {
        return
    }

    $script:lastBuild = Get-Date

    Start-Sleep -Milliseconds 500

    & $using:msbuild $using:sln /t:Build /p:Configuration=Release /m
} | Out-Null

Write-Host "Auto Release Build فعال است..."
Write-Host "برای توقف Ctrl+C بزن."

while ($true) {
    Start-Sleep -Seconds 1
}