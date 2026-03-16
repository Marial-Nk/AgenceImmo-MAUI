$pptx = 'D:\ESA\Cours\25-26\PGBD\App_MAUI_Henriette_Nkondi\BaseMauiApp\Ecran\Ecran_Henriette_Nkondi (1).pptx'
$outDir = 'D:\ESA\Cours\25-26\PGBD\App_MAUI_Henriette_Nkondi\BaseMauiApp\Ecran\slides'
New-Item -ItemType Directory -Force -Path $outDir | Out-Null

$ppt = New-Object -ComObject PowerPoint.Application
$ppt.Visible = [Microsoft.Office.Core.MsoTriState]::msoTrue
$presentation = $ppt.Presentations.Open($pptx)
$presentation.SaveAs($outDir + '\slide', 18)
$presentation.Close()
$ppt.Quit()
Write-Host 'Done'
