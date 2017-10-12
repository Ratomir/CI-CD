param(
    [Parameter(Mandatory = $true)] $source,
    [Parameter(Mandatory = $true)] $database,
    [Parameter(Mandatory = $true)] $username,
    [Parameter(Mandatory = $true)] $pass,
    [Parameter(Mandatory = $true)] $connConfigLoc
)

function changeConnectionString {

    param(
        [Parameter(Mandatory = $true)] $connctionString,
        [Parameter(Mandatory = $true)] $source,
        [Parameter(Mandatory = $true)] $database,
        [Parameter(Mandatory = $true)] $username,
        [Parameter(Mandatory = $true)] $userPass
    )
    $source = $source.Replace("'", "")
    $connctionString = $connctionString.Replace('_DataSource_', $source)
    $connctionString = $connctionString.Replace('_DataBase_', $database)
    $connctionString = $connctionString.Replace('_User_', $username)
    $connctionString = $connctionString.Replace('_Password_', $userPass)
    return $connctionString
}

$doc = (Get-Content $connConfigLoc) -as [Xml]

$root = $doc.get_DocumentElement()
$connStringArray = $root.connectionStrings

$count = ($connStringArray | Measure-Object).Count

for ($i = 0; $i -lt $count -and $count -ne 1; $i++) {
    $root.connectionStrings.add[$i].connectionString = changeConnectionString -connctionString $connStringArray[$i] -source $source -database $database -username $username -userPass $pass;
}

if($root.connectionStrings.add[1] -eq $null){
    $root.connectionStrings.add.connectionString = changeConnectionString -connctionString $root.connectionStrings.add.connectionString -source $source -database $database -username $username -userPass $pass;
}

$doc.Save($connConfigLoc)

Write-Host 'Connection strings are updated' -foregroundcolor green
Write-Host '--------------------------------------'

