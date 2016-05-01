param (
    [Parameter(Mandatory=$true)][PSObject]$data
 )

 ConvertTo-Json -InputObject $data | Out-File -FilePath c:\temp\data.json