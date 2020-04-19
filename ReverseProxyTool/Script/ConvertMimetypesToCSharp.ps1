
$targetFile = ".\mime.types"

$outputFile = ".\mimetype_cs.txt"
$paramLine = ""
$propertyNameList = New-Object "System.Collections.Generic.List[string]"

Remove-Item -Path $outputFile

foreach($line in ((Get-Content $targetFile) -as [string[]])){
    if(([string]::IsNullOrEmpty($line)) -or ($line -eq "types {") -or ($line -eq "}")){
        continue
    }
    if($line -match "[^\/\s]+\/[^\/\s]+\s+[^;]+;$"){
        $paramLine = $line.Trim()
    }else{
        if($line -match "[^\/\s]+\/[^\/\s]+$"){
            $paramLine = $line.Trim()
            continue
        }
        if($line -match "(?<=^\s+)[^;\.\/]+;$"){
            $paramLine += (" " + $line.Trim())
        }
    }
    $paramName = $paramLine.Substring(0, $paramLine.IndexOf(" "))
    $paramValue = $paramLine.Substring($paramLine.IndexOf(" ", 1)).Trim().TrimEnd(";")
    $propertyName = $paramName -replace "[\/\.\-\+]","_"

    if($propertyNameList.Contains($propertyName)){
        for($i = 1; $i -le 100; $i++){
            $tempNewName = $propertyName + "_" + $i.ToString()
            if($propertyNameList.Contains($tempNewName)){
                continue
            }
            $propertyName = $tempNewName
            break
        }
    }
    $propertyNameList.Add($propertyName)

    "[DirectiveParameter(`"${paramName}`")]" | Out-File -FilePath $outputFile -Append
    "public string ${propertyName} { get; set; } = `"${paramValue}`";" | Out-File -FilePath $outputFile -Append

    $paramLine = $null
}

