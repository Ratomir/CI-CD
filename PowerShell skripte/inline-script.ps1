# You can write your powershell scripts inline here. 
# You can also pass predefined and custom variables to this scripts using arguments

param(
    [Parameter(Mandatory = $true)] $connConfigLoc
)
$doc = (Get-Content $connConfigLoc) -as [Xml]
$root = $doc.get_DocumentElement()
Write-Host $root.connectionStrings.add.connectionString