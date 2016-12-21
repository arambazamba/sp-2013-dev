#Variables
$password = ConvertTo-SecureString -string "Pa`$`$w0rd" -asPlainText -force
$webAppAccount = "CONTOSO\SPContent"
$siteOwnerAccount = "CONTOSO\Administrator"

#Clear the screen
Clear-Host

#Load the SharePoint PowerShell snap-in
Write-Host "Verify SharePoint PowerShell snap-in is loaded..." -ForegroundColor White
$snapin = Get-PSSnapin | ?{$_.Name -eq 'Microsoft.SharePoint.PowerShell'}
if ($snapin -eq $null) {
    Write-Host "Loading SharePoint PowerShell snap-in..." -ForegroundColor White
    Add-PSSnapin "Microsoft.SharePoint.PowerShell"
}
Write-Host "SharePoint PowerShell snap-in loaded." -ForegroundColor Green

Write-Host
Write-Host "Configuring host file entries..." -ForegroundColor White

$HostFile = "$($env:windir)\system32\Drivers\etc\hosts" 
Add-Content -Path $HostFile  -Value "127.0.0.1 dev.contoso.com"
Add-Content -Path $HostFile  -Value "127.0.0.1 team.contoso.com"
Add-Content -Path $HostFile  -Value "127.0.0.1 it.contoso.com"
Add-Content -Path $HostFile  -Value "127.0.0.1 managers.contoso.com"
Add-Content -Path $HostFile  -Value "127.0.0.1 marketing.contoso.com"
Add-Content -Path $HostFile  -Value "127.0.0.1 research.contoso.com"
Add-Content -Path $HostFile  -Value "127.0.0.1 sales.contoso.com"

Write-Host
Write-Host "Configuring DNS entries..." -ForegroundColor White

Add-DnsServerPrimaryZone -Name contoso.com -ReplicationScope Domain
Add-DnsServerPrimaryZone -Name contosoapps.com -ReplicationScope Domain
Add-DnsServerResourceRecordA -IPv4Address 172.16.0.5 -Name * -ZoneName contoso.com
Add-DnsServerResourceRecordA -IPv4Address 172.16.0.5 -Name sharepoint -ZoneName contoso.com
Add-DnsServerResourceRecordCName -HostNameAlias sharepoint.contoso.com -Name * -ZoneName contosoapps.com

Write-Host
Write-Host "Configuring SharePoint managed accounts..." -ForegroundColor White

$credential = New-Object System.Management.Automation.PSCredential($webAppAccount, $password)
New-SPManagedAccount -Credential $credential

Write-Host
Write-Host "Creating web application on port 80..." -ForegroundColor White

$authProvider = New-SPAuthenticationProvider
$webApp = New-SPWebApplication -AuthenticationProvider $authProvider -ApplicationPoolAccount (Get-SPManagedAccount $webAppAccount)  -Name "Host Named Site Collection (HNSC) Host Web Application" -Port 80 -ApplicationPool "HNSC Host Application Pool"

Write-Host
Write-Host "Creating developer site collection at http://dev.contoso.com..."
$devSite = New-SPSite http://dev.contoso.com -OwnerAlias $siteOwnerAccount -HostHeaderWebApplication $webApp -Name "Contoso Development Site" -Template "DEV#0"

Write-Host
Write-Host "Creating team site collection at http://team.contoso.com..."
$teamSite = New-SPSite http://team.contoso.com -OwnerAlias $siteOwnerAccount -HostHeaderWebApplication $webApp -Name "Contoso Team Site" -Template "STS#0"

Write-Host
Write-Host "Creating team site collection at http://it.contoso.com..."
$teamSite = New-SPSite http://it.contoso.com -OwnerAlias $siteOwnerAccount -HostHeaderWebApplication $webApp -Name "Contoso Team Site" -Template "STS#0"

Write-Host
Write-Host "Creating team site collection at http://managers.contoso.com..."
$teamSite = New-SPSite http://managers.contoso.com -OwnerAlias $siteOwnerAccount -HostHeaderWebApplication $webApp -Name "Contoso Team Site" -Template "STS#0"

Write-Host
Write-Host "Creating team site collection at http://marketing.contoso.com..."
$teamSite = New-SPSite http://marketing.contoso.com -OwnerAlias $siteOwnerAccount -HostHeaderWebApplication $webApp -Name "Contoso Team Site" -Template "STS#0"

Write-Host
Write-Host "Creating team site collection at http://research.contoso.com..."
$teamSite = New-SPSite http://research.contoso.com -OwnerAlias $siteOwnerAccount -HostHeaderWebApplication $webApp -Name "Contoso Team Site" -Template "STS#0"

Write-Host
Write-Host "Creating team site collection at http://sales.contoso.com..."
$teamSite = New-SPSite http://sales.contoso.com -OwnerAlias $siteOwnerAccount -HostHeaderWebApplication $webApp -Name "Contoso Team Site" -Template "STS#0"

#Configure StateService
Write-Host
Write-Host "Calling script to configure State service..."
.{.\ConfigureStateService.ps1}

#Configure logging
Write-Host
Write-Host "Calling script to configure logging..."
#.{.\ConfigureLogging.ps1}
Write-Host "Skippped configuring logging."

#Install setup solution
Write-Host
Write-Host "Installing Mod05Setup solution..."
Add-SPSolution -LiteralPath E:\setupfiles\Mod05Setup.wsp
Install-SPSolution -Id Mod05Setup.wsp -GACDeployment

iisreset

#Install the Site Columns Feature.
#Write-Host
#Write-Host "Installing the Site Columns Feature..."
#Install-SPFeature -path "Mod05Setup_Site Columns"

#Install the Site Content Type Feature.
#Write-Host
#Write-Host "Installing the Site Content Type Feature..."
#Install-SPFeature -path "Mod05Setup_Site Content Type"

#Install the Contoso Expenses List Feature.
#Write-Host
#Write-Host "Installing the Contoso Expenses List Feature..."
#Install-SPFeature -path "Mod05Setup_Contoso Expenses List"

#Install the Managers Expense Overview List Feature.
#Write-Host
#Write-Host "Installing the Managers Expense Overview List Feature..."
#Install-SPFeature -path "Mod05Setup_Managers Expense Overview List"

#iisreset

#Enable the Features on each site.
Write-Host
Write-Host "Enabling Features at http://it.contoso.com ..."
Enable-SPFeature -id "Mod05Setup_Site Columns" -url http://it.contoso.com 
Enable-SPFeature -id "Mod05Setup_Site Content Type" -url http://it.contoso.com 
Enable-SPFeature -id "Mod05Setup_Contoso Expenses List" -url http://it.contoso.com 

Write-Host
Write-Host "Enabling Features at http://managers.contoso.com ..."
Enable-SPFeature -id "Mod05Setup_Site Columns" -url http://managers.contoso.com 
Enable-SPFeature -id "Mod05Setup_Site Content Type" -url http://managers.contoso.com 
Enable-SPFeature -id "Mod05Setup_Contoso Expenses List" -url http://managers.contoso.com 
Enable-SPFeature -id "Mod05Setup_Managers Expense Overview List" -url http://managers.contoso.com 

Write-Host
Write-Host "Enabling Features at http://marketing.contoso.com ..."
Enable-SPFeature -id "Mod05Setup_Site Columns" -url http://marketing.contoso.com 
Enable-SPFeature -id "Mod05Setup_Site Content Type" -url http://marketing.contoso.com 
Enable-SPFeature -id "Mod05Setup_Contoso Expenses List" -url http://marketing.contoso.com 

Write-Host
Write-Host "Enabling Features at http://research.contoso.com ..."
Enable-SPFeature -id "Mod05Setup_Site Columns" -url http://research.contoso.com 
Enable-SPFeature -id "Mod05Setup_Site Content Type" -url http://research.contoso.com 
Enable-SPFeature -id "Mod05Setup_Contoso Expenses List" -url http://research.contoso.com 

Write-Host
Write-Host "Enabling Features at http://sales.contoso.com ..."
Enable-SPFeature -id "Mod05Setup_Site Columns" -url http://sales.contoso.com 
Enable-SPFeature -id "Mod05Setup_Site Content Type" -url http://sales.contoso.com 
Enable-SPFeature -id "Mod05Setup_Contoso Expenses List" -url http://sales.contoso.com 

Write-Host
Write-Host "Adding items to the expenses list on http://it.contoso.com"
$web = Get-SPWeb http://it.contoso.com
$list = $web.Lists["Contoso Expenses"]
$item1 = $list.Items.Add()
$item1["Title"] = "Stationery"
$item1["Invoice Number"] = "357"
$item1["Supplier Name"] = "Fabrikam"
$item1["Invoice Total"] = 1000.56
$item1["Invoice Date"] = "2013-06-01 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "IT Equipment"
$item1["Invoice Number"] = "1976"
$item1["Supplier Name"] = "LitWare Inc"
$item1["Invoice Total"] = 236.76
$item1["Invoice Date"] = "2013-05-25 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "Travel"
$item1["Invoice Number"] = "63AS450"
$item1["Supplier Name"] = "Parnell Aerospace"
$item1["Invoice Total"] = 500.00
$item1["Invoice Date"] = "2013-06-06 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "Mobile calls"
$item1["Invoice Number"] = "3452386"
$item1["Supplier Name"] = "Phone Company"
$item1["Invoice Total"] = 9.24
$item1["Invoice Date"] = "2013-05-25 00:00:00"
$item1.Update()

Write-Host
Write-Host "Adding items to the expenses list on http://managers.contoso.com"
$web = Get-SPWeb http://managers.contoso.com
$list = $web.Lists["Contoso Expenses"]
$item1 = $list.Items.Add()
$item1["Title"] = "Stationery"
$item1["Invoice Number"] = "679"
$item1["Supplier Name"] = "Fabrikam"
$item1["Invoice Total"] = 634.25
$item1["Invoice Date"] = "2013-05-29 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "Meeting"
$item1["Invoice Number"] = "132A"
$item1["Supplier Name"] = "Fourth Coffee"
$item1["Invoice Total"] = 236.76
$item1["Invoice Date"] = "2013-05-25 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "Meeting"
$item1["Invoice Number"] = "133A"
$item1["Supplier Name"] = "Coho Winery"
$item1["Invoice Total"] = 500.00
$item1["Invoice Date"] = "2013-06-06 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "New computer"
$item1["Invoice Number"] = "AH6987"
$item1["Supplier Name"] = "Awesome Computers"
$item1["Invoice Total"] = 1526.88
$item1["Invoice Date"] = "2013-05-01 00:00:00"
$item1.Update()

Write-Host
Write-Host "Adding items to the expenses list on http://marketing.contoso.com"
$web = Get-SPWeb http://marketing.contoso.com
$list = $web.Lists["Contoso Expenses"]
$item1 = $list.Items.Add()
$item1["Title"] = "Business Trip"
$item1["Invoice Number"] = "ASH790"
$item1["Supplier Name"] = "Alpine Ski House"
$item1["Invoice Total"] = 5669.53
$item1["Invoice Date"] = "2013-04-03 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "Demonstration Equipment"
$item1["Invoice Number"] = "INTERNAL"
$item1["Supplier Name"] = "Contoso Pharmaceuticals"
$item1["Invoice Total"] = 9658.56
$item1["Invoice Date"] = "2013-06-01 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "Goodwill gift"
$item1["Invoice Number"] = "133A"
$item1["Supplier Name"] = "Coho Vineyard"
$item1["Invoice Total"] = 100.25
$item1["Invoice Date"] = "2013-06-13 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "Office supplies"
$item1["Invoice Number"] = "WWI990"
$item1["Supplier Name"] = "Wide World Importers"
$item1["Invoice Total"] = 26.15
$item1["Invoice Date"] = "2013-06-10 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "Sales documentation"
$item1["Invoice Number"] = "46729356"
$item1["Supplier Name"] = "Lucerne Publishing"
$item1["Invoice Total"] = 968.87
$item1["Invoice Date"] = "2013-06-10 00:00:00"
$item1.Update()

Write-Host
Write-Host "Adding items to the expenses list on http://research.contoso.com"
$web = Get-SPWeb http://research.contoso.com
$list = $web.Lists["Contoso Expenses"]
$item1 = $list.Items.Add()
$item1["Title"] = "Stationery"
$item1["Invoice Number"] = "968"
$item1["Supplier Name"] = "Fabrikam"
$item1["Invoice Total"] = 124.76
$item1["Invoice Date"] = "2013-06-15 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "Meeting"
$item1["Invoice Number"] = "158A"
$item1["Supplier Name"] = "Fourth Coffee"
$item1["Invoice Total"] = 50.68
$item1["Invoice Date"] = "2013-05-23 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "Supplies"
$item1["Invoice Number"] = "TR357"
$item1["Supplier Name"] = "Trey Research Inc."
$item1["Invoice Total"] = 24563.25
$item1["Invoice Date"] = "2013-01-05 00:00:00"
$item1.Update()
$item1 = $list.Items.Add()
$item1["Title"] = "New computer"
$item1["Invoice Number"] = "AH7638"
$item1["Supplier Name"] = "Awesome Computers"
$item1["Invoice Total"] = 758.55
$item1["Invoice Date"] = "2013-05-01 00:00:00"
$item1.Update()

Write-Host
Write-Host "Finished." -ForegroundColor Green
# SIG # Begin signature block
# MIIavQYJKoZIhvcNAQcCoIIarjCCGqoCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUnZy49E3xR1w+T3cRLzsQQ3sn
# AVygghWCMIIEwzCCA6ugAwIBAgITMwAAACs5MkjBsslI8wAAAAAAKzANBgkqhkiG
# 9w0BAQUFADB3MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4G
# A1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSEw
# HwYDVQQDExhNaWNyb3NvZnQgVGltZS1TdGFtcCBQQ0EwHhcNMTIwOTA0MjExMjM0
# WhcNMTMxMjA0MjExMjM0WjCBszELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hp
# bmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jw
# b3JhdGlvbjENMAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNpcGhlciBEU0UgRVNO
# OkMwRjQtMzA4Ni1ERUY4MSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBT
# ZXJ2aWNlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAprYwDgNlrlBa
# hmuFn0ihHsnA7l5JB4XgcJZ8vrlfYl8GJtOLObsYIqUukq3YS4g6Gq+bg67IXjmM
# wjJ7FnjtNzg68WL7aIICaOzru0CKsf6hLDZiYHA5YGIO+8YYOG+wktZADYCmDXiL
# NmuGiiYXGP+w6026uykT5lxIjnBGNib+NDWrNOH32thc6pl9MbdNH1frfNaVDWYM
# Hg4yFz4s1YChzuv3mJEC3MFf/TiA+Dl/XWTKN1w7UVtdhV/OHhz7NL5f5ShVcFSc
# uOx8AFVGWyiYKFZM4fG6CRmWgUgqMMj3MyBs52nDs9TDTs8wHjfUmFLUqSNFsq5c
# QUlPtGJokwIDAQABo4IBCTCCAQUwHQYDVR0OBBYEFKUYM1M/lWChQxbvjsav0iu6
# nljQMB8GA1UdIwQYMBaAFCM0+NlSRnAK7UD7dvuzK7DDNbMPMFQGA1UdHwRNMEsw
# SaBHoEWGQ2h0dHA6Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3Rz
# L01pY3Jvc29mdFRpbWVTdGFtcFBDQS5jcmwwWAYIKwYBBQUHAQEETDBKMEgGCCsG
# AQUFBzAChjxodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2NlcnRzL01pY3Jv
# c29mdFRpbWVTdGFtcFBDQS5jcnQwEwYDVR0lBAwwCgYIKwYBBQUHAwgwDQYJKoZI
# hvcNAQEFBQADggEBAH7MsHvlL77nVrXPc9uqUtEWOca0zfrX/h5ltedI85tGiAVm
# aiaGXv6HWNzGY444gPQIRnwrc7EOv0Gqy8eqlKQ38GQ54cXV+c4HzqvkJfBprtRG
# 4v5mMjzXl8UyIfruGiWgXgxCLBEzOoKD/e0ds77OkaSRJXG5q3Kwnq/kzwBiiXCp
# uEpQjO4vImSlqOZNa5UsHHnsp6Mx2pBgkKRu/pMCDT8sJA3GaiaBUYNKELt1Y0Sq
# aQjGA+vizwvtVjrs73KnCgz0ANMiuK8icrPnxJwLKKCAyuPh1zlmMOdGFxjn+oL6
# WQt6vKgN/hz/A4tjsk0SAiNPLbOFhDvioUfozxUwggTsMIID1KADAgECAhMzAAAA
# sBGvCovQO5/dAAEAAACwMA0GCSqGSIb3DQEBBQUAMHkxCzAJBgNVBAYTAlVTMRMw
# EQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVN
# aWNyb3NvZnQgQ29ycG9yYXRpb24xIzAhBgNVBAMTGk1pY3Jvc29mdCBDb2RlIFNp
# Z25pbmcgUENBMB4XDTEzMDEyNDIyMzMzOVoXDTE0MDQyNDIyMzMzOVowgYMxCzAJ
# BgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25k
# MR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xDTALBgNVBAsTBE1PUFIx
# HjAcBgNVBAMTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjCCASIwDQYJKoZIhvcNAQEB
# BQADggEPADCCAQoCggEBAOivXKIgDfgofLwFe3+t7ut2rChTPzrbQH2zjjPmVz+l
# URU0VKXPtIupP6g34S1Q7TUWTu9NetsTdoiwLPBZXKnr4dcpdeQbhSeb8/gtnkE2
# KwtA+747urlcdZMWUkvKM8U3sPPrfqj1QRVcCGUdITfwLLoiCxCxEJ13IoWEfE+5
# G5Cw9aP+i/QMmk6g9ckKIeKq4wE2R/0vgmqBA/WpNdyUV537S9QOgts4jxL+49Z6
# dIhk4WLEJS4qrp0YHw4etsKvJLQOULzeHJNcSaZ5tbbbzvlweygBhLgqKc+/qQUF
# 4eAPcU39rVwjgynrx8VKyOgnhNN+xkMLlQAFsU9lccUCAwEAAaOCAWAwggFcMBMG
# A1UdJQQMMAoGCCsGAQUFBwMDMB0GA1UdDgQWBBRZcaZaM03amAeA/4Qevof5cjJB
# 8jBRBgNVHREESjBIpEYwRDENMAsGA1UECxMETU9QUjEzMDEGA1UEBRMqMzE1OTUr
# NGZhZjBiNzEtYWQzNy00YWEzLWE2NzEtNzZiYzA1MjM0NGFkMB8GA1UdIwQYMBaA
# FMsR6MrStBZYAck3LjMWFrlMmgofMFYGA1UdHwRPME0wS6BJoEeGRWh0dHA6Ly9j
# cmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3RzL01pY0NvZFNpZ1BDQV8w
# OC0zMS0yMDEwLmNybDBaBggrBgEFBQcBAQROMEwwSgYIKwYBBQUHMAKGPmh0dHA6
# Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljQ29kU2lnUENBXzA4LTMx
# LTIwMTAuY3J0MA0GCSqGSIb3DQEBBQUAA4IBAQAx124qElczgdWdxuv5OtRETQie
# 7l7falu3ec8CnLx2aJ6QoZwLw3+ijPFNupU5+w3g4Zv0XSQPG42IFTp8263Os8ls
# ujksRX0kEVQmMA0N/0fqAwfl5GZdLHudHakQ+hywdPJPaWueqSSE2u2WoN9zpO9q
# GqxLYp7xfMAUf0jNTbJE+fA8k21C2Oh85hegm2hoCSj5ApfvEQO6Z1Ktwemzc6bS
# Y81K4j7k8079/6HguwITO10g3lU/o66QQDE4dSheBKlGbeb1enlAvR/N6EXVruJd
# PvV1x+ZmY2DM1ZqEh40kMPfvNNBjHbFCZ0oOS786Du+2lTqnOOQlkgimiGaCMIIF
# vDCCA6SgAwIBAgIKYTMmGgAAAAAAMTANBgkqhkiG9w0BAQUFADBfMRMwEQYKCZIm
# iZPyLGQBGRYDY29tMRkwFwYKCZImiZPyLGQBGRYJbWljcm9zb2Z0MS0wKwYDVQQD
# EyRNaWNyb3NvZnQgUm9vdCBDZXJ0aWZpY2F0ZSBBdXRob3JpdHkwHhcNMTAwODMx
# MjIxOTMyWhcNMjAwODMxMjIyOTMyWjB5MQswCQYDVQQGEwJVUzETMBEGA1UECBMK
# V2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0
# IENvcnBvcmF0aW9uMSMwIQYDVQQDExpNaWNyb3NvZnQgQ29kZSBTaWduaW5nIFBD
# QTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBALJyWVwZMGS/HZpgICBC
# mXZTbD4b1m/My/Hqa/6XFhDg3zp0gxq3L6Ay7P/ewkJOI9VyANs1VwqJyq4gSfTw
# aKxNS42lvXlLcZtHB9r9Jd+ddYjPqnNEf9eB2/O98jakyVxF3K+tPeAoaJcap6Vy
# c1bxF5Tk/TWUcqDWdl8ed0WDhTgW0HNbBbpnUo2lsmkv2hkL/pJ0KeJ2L1TdFDBZ
# +NKNYv3LyV9GMVC5JxPkQDDPcikQKCLHN049oDI9kM2hOAaFXE5WgigqBTK3S9dP
# Y+fSLWLxRT3nrAgA9kahntFbjCZT6HqqSvJGzzc8OJ60d1ylF56NyxGPVjzBrAlf
# A9MCAwEAAaOCAV4wggFaMA8GA1UdEwEB/wQFMAMBAf8wHQYDVR0OBBYEFMsR6MrS
# tBZYAck3LjMWFrlMmgofMAsGA1UdDwQEAwIBhjASBgkrBgEEAYI3FQEEBQIDAQAB
# MCMGCSsGAQQBgjcVAgQWBBT90TFO0yaKleGYYDuoMW+mPLzYLTAZBgkrBgEEAYI3
# FAIEDB4KAFMAdQBiAEMAQTAfBgNVHSMEGDAWgBQOrIJgQFYnl+UlE/wq4QpTlVnk
# pDBQBgNVHR8ESTBHMEWgQ6BBhj9odHRwOi8vY3JsLm1pY3Jvc29mdC5jb20vcGtp
# L2NybC9wcm9kdWN0cy9taWNyb3NvZnRyb290Y2VydC5jcmwwVAYIKwYBBQUHAQEE
# SDBGMEQGCCsGAQUFBzAChjhodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2Nl
# cnRzL01pY3Jvc29mdFJvb3RDZXJ0LmNydDANBgkqhkiG9w0BAQUFAAOCAgEAWTk+
# fyZGr+tvQLEytWrrDi9uqEn361917Uw7LddDrQv+y+ktMaMjzHxQmIAhXaw9L0y6
# oqhWnONwu7i0+Hm1SXL3PupBf8rhDBdpy6WcIC36C1DEVs0t40rSvHDnqA2iA6VW
# 4LiKS1fylUKc8fPv7uOGHzQ8uFaa8FMjhSqkghyT4pQHHfLiTviMocroE6WRTsgb
# 0o9ylSpxbZsa+BzwU9ZnzCL/XB3Nooy9J7J5Y1ZEolHN+emjWFbdmwJFRC9f9Nqu
# 1IIybvyklRPk62nnqaIsvsgrEA5ljpnb9aL6EiYJZTiU8XofSrvR4Vbo0HiWGFzJ
# NRZf3ZMdSY4tvq00RBzuEBUaAF3dNVshzpjHCe6FDoxPbQ4TTj18KUicctHzbMrB
# 7HCjV5JXfZSNoBtIA1r3z6NnCnSlNu0tLxfI5nI3EvRvsTxngvlSso0zFmUeDord
# EN5k9G/ORtTTF+l5xAS00/ss3x+KnqwK+xMnQK3k+eGpf0a7B2BHZWBATrBC7E7t
# s3Z52Ao0CW0cgDEf4g5U3eWh++VHEK1kmP9QFi58vwUheuKVQSdpw5OPlcmN2Jsh
# rg1cnPCiroZogwxqLbt2awAdlq3yFnv2FoMkuYjPaqhHMS+a3ONxPdcAfmJH0c6I
# ybgY+g5yjcGjPa8CQGr/aZuW4hCoELQ3UAjWwz0wggYHMIID76ADAgECAgphFmg0
# AAAAAAAcMA0GCSqGSIb3DQEBBQUAMF8xEzARBgoJkiaJk/IsZAEZFgNjb20xGTAX
# BgoJkiaJk/IsZAEZFgltaWNyb3NvZnQxLTArBgNVBAMTJE1pY3Jvc29mdCBSb290
# IENlcnRpZmljYXRlIEF1dGhvcml0eTAeFw0wNzA0MDMxMjUzMDlaFw0yMTA0MDMx
# MzAzMDlaMHcxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYD
# VQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xITAf
# BgNVBAMTGE1pY3Jvc29mdCBUaW1lLVN0YW1wIFBDQTCCASIwDQYJKoZIhvcNAQEB
# BQADggEPADCCAQoCggEBAJ+hbLHf20iSKnxrLhnhveLjxZlRI1Ctzt0YTiQP7tGn
# 0UytdDAgEesH1VSVFUmUG0KSrphcMCbaAGvoe73siQcP9w4EmPCJzB/LMySHnfL0
# Zxws/HvniB3q506jocEjU8qN+kXPCdBer9CwQgSi+aZsk2fXKNxGU7CG0OUoRi4n
# rIZPVVIM5AMs+2qQkDBuh/NZMJ36ftaXs+ghl3740hPzCLdTbVK0RZCfSABKR2YR
# JylmqJfk0waBSqL5hKcRRxQJgp+E7VV4/gGaHVAIhQAQMEbtt94jRrvELVSfrx54
# QTF3zJvfO4OToWECtR0Nsfz3m7IBziJLVP/5BcPCIAsCAwEAAaOCAaswggGnMA8G
# A1UdEwEB/wQFMAMBAf8wHQYDVR0OBBYEFCM0+NlSRnAK7UD7dvuzK7DDNbMPMAsG
# A1UdDwQEAwIBhjAQBgkrBgEEAYI3FQEEAwIBADCBmAYDVR0jBIGQMIGNgBQOrIJg
# QFYnl+UlE/wq4QpTlVnkpKFjpGEwXzETMBEGCgmSJomT8ixkARkWA2NvbTEZMBcG
# CgmSJomT8ixkARkWCW1pY3Jvc29mdDEtMCsGA1UEAxMkTWljcm9zb2Z0IFJvb3Qg
# Q2VydGlmaWNhdGUgQXV0aG9yaXR5ghB5rRahSqClrUxzWPQHEy5lMFAGA1UdHwRJ
# MEcwRaBDoEGGP2h0dHA6Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1
# Y3RzL21pY3Jvc29mdHJvb3RjZXJ0LmNybDBUBggrBgEFBQcBAQRIMEYwRAYIKwYB
# BQUHMAKGOGh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljcm9z
# b2Z0Um9vdENlcnQuY3J0MBMGA1UdJQQMMAoGCCsGAQUFBwMIMA0GCSqGSIb3DQEB
# BQUAA4ICAQAQl4rDXANENt3ptK132855UU0BsS50cVttDBOrzr57j7gu1BKijG1i
# uFcCy04gE1CZ3XpA4le7r1iaHOEdAYasu3jyi9DsOwHu4r6PCgXIjUji8FMV3U+r
# kuTnjWrVgMHmlPIGL4UD6ZEqJCJw+/b85HiZLg33B+JwvBhOnY5rCnKVuKE5nGct
# xVEO6mJcPxaYiyA/4gcaMvnMMUp2MT0rcgvI6nA9/4UKE9/CCmGO8Ne4F+tOi3/F
# NSteo7/rvH0LQnvUU3Ih7jDKu3hlXFsBFwoUDtLaFJj1PLlmWLMtL+f5hYbMUVbo
# nXCUbKw5TNT2eb+qGHpiKe+imyk0BncaYsk9Hm0fgvALxyy7z0Oz5fnsfbXjpKh0
# NbhOxXEjEiZ2CzxSjHFaRkMUvLOzsE1nyJ9C/4B5IYCeFTBm6EISXhrIniIh0EPp
# K+m79EjMLNTYMoBMJipIJF9a6lbvpt6Znco6b72BJ3QGEe52Ib+bgsEnVLaxaj2J
# oXZhtG6hE6a/qkfwEm/9ijJssv7fUciMI8lmvZ0dhxJkAj0tr1mPuOQh5bWwymO0
# eFQF1EEuUKyUsKV4q7OglnUa2ZKHE3UiLzKoCG6gW4wlv6DvhMoh1useT8ma7kng
# 9wFlb4kLfchpyOZu6qeXzjEp/w7FW1zYTRuh2Povnj8uVRZryROj/TGCBKUwggSh
# AgEBMIGQMHkxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYD
# VQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xIzAh
# BgNVBAMTGk1pY3Jvc29mdCBDb2RlIFNpZ25pbmcgUENBAhMzAAAAsBGvCovQO5/d
# AAEAAACwMAkGBSsOAwIaBQCggb4wGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQw
# HAYKKwYBBAGCNwIBCzEOMAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFKCm
# zwKNFW+D+JiMmCAGtIT1ZDppMF4GCisGAQQBgjcCAQwxUDBOoCaAJABNAGkAYwBy
# AG8AcwBvAGYAdAAgAEwAZQBhAHIAbgBpAG4AZ6EkgCJodHRwOi8vd3d3Lm1pY3Jv
# c29mdC5jb20vbGVhcm5pbmcgMA0GCSqGSIb3DQEBAQUABIIBABMhyaEwll80f4Il
# IwDt+G1Sw3tUyQ7nKdSFQ/ZxFGclvvreKNeHrBoI+JuUpKbqhoYNHhDj7JUuyxmf
# bAJ/9+t5hi/dD8nuyY3O5ZaHkfXLzkEt7dazqypJh10aOTy/FlMkKqxZdTMAd9Ig
# f7ATTSzLXOMVu0FWf+GtIkMy+dIOg5qf2tfYXLMHC9s+qsQKA3YxEidtBYiiLnyB
# iE3Zoe69BjGvU8cG+umkzHeSc/OCYMOr0aj/czxCLYwvvu3idic9w7PulcZzWWLi
# Z6H4fEs9I2rJ5UeTgh1D98R3t+vDvGeHZCanuP6TlY1Wqmt9ozLPX6B7nY4yHFNF
# 9PD/TM6hggIoMIICJAYJKoZIhvcNAQkGMYICFTCCAhECAQEwgY4wdzELMAkGA1UE
# BhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAc
# BgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEhMB8GA1UEAxMYTWljcm9zb2Z0
# IFRpbWUtU3RhbXAgUENBAhMzAAAAKzkySMGyyUjzAAAAAAArMAkGBSsOAwIaBQCg
# XTAYBgkqhkiG9w0BCQMxCwYJKoZIhvcNAQcBMBwGCSqGSIb3DQEJBTEPFw0xMzA3
# MDExODAwMTFaMCMGCSqGSIb3DQEJBDEWBBSwlXntVX2l4P/7/VyYfOg8dV9GUzAN
# BgkqhkiG9w0BAQUFAASCAQA7Ee5KlNCCMIWsS+beFEgFoL4tfIlbHveoepw5qbiv
# RZ9wx1eXSNx3FOUpdIRtuSQUtCJXFmA636zBJPST9q1R2bA+MXMxo8Zl8l8Ta69W
# j3+NqJKTkJZFsRyhy1Gtfs1hmj0skWNzDFuaiyN8L7Dm/UawwLFCfUAfjWNsz9S1
# LVzvtI/Q9eIlLR9ZUOMR2OXzpNyXmUOyt6rEY1afIgnZPWojEB4tIezlHg/2B8K0
# 7LyKgn1wFpA6TueYTI/vFqJX5pUkgXd8j+44VxgvgjnLvGdSTBLXidYkfAsgeKXV
# 8dX6B6Mw4u8h+ZcNzl9VZA/av4W9f52FqIhrZ5GLsdc8
# SIG # End signature block
