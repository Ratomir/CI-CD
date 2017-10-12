param(
    [Parameter(Mandatory=$true)] $connConfigLoc)

try {
    $doc = (Get-Content $connConfigLoc) -as [Xml]

    $root = $doc.get_DocumentElement()
    $temp = 'Server=_DataSource_;Initial Catalog=_DataBase_;Persist Security Info=False;User ID=_User_;Password=_Password_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'

    $connStringArray = $root.connectionStrings
    $count = ($connStringArray | Measure-Object).Count

    for ($i = 0; $i -lt $count -and $count -ne 1; $i++) {
        $root.connectionStrings.add[$i].connectionString = $temp
    }

    if($root.connectionStrings.add[1] -eq $null){
        $root.connectionStrings.add.connectionString = $temp
    }

    $doc.Save($connConfigLoc)

    Write-Host 'Changeing for connections strings done' -foregroundcolor green
    Write-Host '--------------------------------------'
}
catch {
    Write-Error -Message "Document Config can not be find. Please, try other location. Check format of xml document."
}