#This script creates the site columns, content types, lists, and items necessary for Lab 10

#Load the SharePoint PowerShell snap-in
Write-Host "Verify SharePoint PowerShell snap-in is loaded..." -ForegroundColor White
$snapin = Get-PSSnapin | ?{$_.Name -eq 'Microsoft.SharePoint.PowerShell'}
if ($snapin -eq $null) {
    Write-Host "Loading SharePoint PowerShell snap-in..." -ForegroundColor White
    Add-PSSnapin "Microsoft.SharePoint.PowerShell"
}
Write-Host "SharePoint PowerShell snap-in loaded." -ForegroundColor Green

#Use an SPAssignment to ensure disposal of site collections and webs
Start-SPAssignment -Global

Write-Host
Write-Host "Getting team site at http://dev.contoso.com/..."
$web = Get-SPWeb http://dev.contoso.com/
#Store the custom list template for later
$customListTemplate = $web.ListTemplates["Custom List"]

Write-Host
Write-Host "Creating Regions site columns..." -ForegroundColor Yellow

$fieldXMLString = '<Field Type="Text"
Name="CurrencyName"
Description="Use this column to specify the name of a currency for a region"
DisplayName="Currency Name"
StaticName="CurrencyName"
Group="Custom Columns"
Hidden="FALSE"
Required="FALSE"
Sealed="FALSE"
ShowInDisplayForm="TRUE"
ShowInEditForm="TRUE"
ShowInListSettings="TRUE"
ShowInNewForm="TRUE"></Field>'

Write-Host "Creating CurrencyName site column..."
$web.Fields.AddFieldAsXml($fieldXMLString)

$fieldXMLString = '<Field Type="Number"
Name="ExchangeRate"
Description="Use this column to record the exchange rate for a local currency to dollars"
DisplayName="Exchange Rate"
StaticName="ExchangeRate"
Group="Custom Columns"
Hidden="FALSE"
Required="FALSE"
Sealed="FALSE"
ShowInDisplayForm="TRUE"
ShowInEditForm="TRUE"
ShowInListSettings="TRUE"
ShowInNewForm="TRUE"></Field>'

Write-Host "Creating ExchangeRate site column..."
$web.Fields.AddFieldAsXml($fieldXMLString)

Write-Host
Write-Host "Creating Region content type..." -ForegroundColor Yellow
$parentType = $web.AvailableContentTypes["Item"]
$regionContentType = New-Object Microsoft.SharePoint.SPContentType($parentType, $web.ContentTypes, "Region")
$regionContentType.Group = "Custom Content Types"
$web.ContentTypes.Add($regionContentType)

$currencyNameColumn = $web.Fields.GetField("Currency Name")
$currencyNameFieldLink = New-Object Microsoft.SharePoint.SPFieldLink($currencyNameColumn) 
$regionContentType.FieldLinks.Add($currencyNameFieldLink) 
$regionContentType.Update()


$exchangeRateColumn = $web.Fields.GetField("Exchange Rate")
$exchangeRateFieldLink = New-Object Microsoft.SharePoint.SPFieldLink($exchangeRateColumn) 
$regionContentType.FieldLinks.Add($exchangeRateFieldLink) 
$regionContentType.Update() 

Write-Host
Write-Host "Creating Regions list..." -ForegroundColor Yellow
$web.Lists.Add("Regions", "Use this list to store regions and their currency details", $customListTemplate)
$regionsList = $web.Lists["Regions"]
$regionsList.Title = "Regions"
$regionsList.Update()

Write-Host "Adding the Region content type to the Regions list..."
$regionsList.ContentTypesEnabled = $true
$regionsList.Update()
$regionsList.ContentTypes.Add($regionContentType)
$regionsList.Update()

Write-Host
Write-Host "Adding some regions..."
$region1 = $regionsList.Items.Add()
$region1["ContentTypeId"] = $regionContentType.Id
$region1.Update()
$region1["Title"] = "USA"
$region1["Currency Name"] = "US Dollars"
$region1["Exchange Rate"] = 1
$region1.Update()
$region2 = $regionsList.Items.Add()
$region2["ContentTypeId"] = $regionContentType.Id
$region2.Update()
$region2["Title"] = "UK"
$region2["Currency Name"] = "Sterling"
$region2["Exchange Rate"] = 0.65
$region2.Update()
$region3 = $regionsList.Items.Add()
$region3["ContentTypeId"] = $regionContentType.Id
$region3.Update()
$region3["Title"] = "Eurozone"
$region3["Currency Name"] = "Euros"
$region3["Exchange Rate"] = 0.76
$region3.Update()

Write-Host
Write-Host "Creating Sales site columns..." -ForegroundColor Yellow

$fieldXMLString = '<Field Type="Note"
Name="TransactionDescription"
Description="Use this column to describe a sale or a purchase"
DisplayName="Transaction Description"
StaticName="TransactionDescription"
Group="Custom Columns"
Hidden="FALSE"
Required="FALSE"
Sealed="FALSE"
ShowInDisplayForm="TRUE"
ShowInEditForm="TRUE"
ShowInListSettings="TRUE"
ShowInNewForm="TRUE"></Field>'

Write-Host "Creating TransactionDecription site column..."
$web.Fields.AddFieldAsXml($fieldXMLString)

$fieldXMLString = '<Field Type="Text"
Name="CustomerName"
Description="Use this column to specify the customer for a sale"
DisplayName="Customer Name"
StaticName="CustomerName"
Group="Custom Columns"
Hidden="FALSE"
Required="FALSE"
Sealed="FALSE"
ShowInDisplayForm="TRUE"
ShowInEditForm="TRUE"
ShowInListSettings="TRUE"
ShowInNewForm="TRUE"></Field>'

Write-Host "Creating CustomerName site column..."
$web.Fields.AddFieldAsXml($fieldXMLString)

$fieldXMLString = '<Field Type="Lookup"
Name="RegionLookup"
Description="Use this column to specify the region for a sale"
DisplayName="Region"
StaticName="RegionLookup"
Group="Custom Columns"
List="Lists/Regions"
ShowField="Title"
Hidden="FALSE"
Required="FALSE"
Sealed="FALSE"
ShowInDisplayForm="TRUE"
ShowInEditForm="TRUE"
ShowInListSettings="TRUE"
ShowInNewForm="TRUE"></Field>'

Write-Host "Creating RegionsLookup site column..."
$web.Fields.AddLookup("RegionLookup", $regionsList.Id, $false)
#$web.Fields.AddFieldAsXml($fieldXMLString)

$fieldXMLString = '<Field Type="Number"
Name="Amount"
Description="Use this column to record the amount of a purchase in the local currency"
DisplayName="Amount"
StaticName="Amount"
Group="Custom Columns"
Hidden="FALSE"
Required="FALSE"
Sealed="FALSE"
ShowInDisplayForm="TRUE"
ShowInEditForm="TRUE"
ShowInListSettings="TRUE"
ShowInNewForm="TRUE"></Field>'

Write-Host "Creating Amount site column..."
$web.Fields.AddFieldAsXml($fieldXMLString)

Write-Host
Write-Host "Creating Sale content type..." -ForegroundColor Yellow
$parentType = $web.AvailableContentTypes["Item"]
$saleContentType = New-Object Microsoft.SharePoint.SPContentType($parentType, $web.ContentTypes, "Sale")
$saleContentType.Group = "Custom Content Types"
$web.ContentTypes.Add($saleContentType)

$transactionDescriptionColumn = $web.Fields.GetField("Transaction Description")
$transactionDescriptionFieldLink = New-Object Microsoft.SharePoint.SPFieldLink($transactionDescriptionColumn) 
$saleContentType.FieldLinks.Add($transactionDescriptionFieldLink) 
$saleContentType.Update() 

$customerNameColumn = $web.Fields.GetField("Customer Name")
$customerNameFieldLink = New-Object Microsoft.SharePoint.SPFieldLink($customerNameColumn) 
$saleContentType.FieldLinks.Add($customerNameFieldLink)
$saleContentType.Update()  

$regionsLookupColumn = $web.Fields.GetField("RegionLookup")
$regionsLookupFieldLink = New-Object Microsoft.SharePoint.SPFieldLink($regionsLookupColumn) 
$saleContentType.FieldLinks.Add($regionsLookupFieldLink)
$saleContentType.Update() 

$amountColumn = $web.Fields.GetField("Amount")
$amountFieldLink = New-Object Microsoft.SharePoint.SPFieldLink($amountColumn) 
$saleContentType.FieldLinks.Add($amountFieldLink)
$saleContentType.Update() 

Write-Host
Write-Host "Creating Sales Ledger list..." -ForegroundColor Yellow
$web.Lists.Add("SalesLedger", "Use this list to store sales and their details", $customListTemplate)
$salesList = $web.Lists["SalesLedger"]
$salesList.Title = "Sales Ledger"
$salesList.Update()

Write-Host "Adding the Sale content type to the Sales Ledger list..."
$salesList.ContentTypesEnabled = $true
$salesList.Update()
$salesList.ContentTypes.Add($saleContentType)
$salesList.Update()

Write-Host
Write-Host "Adding some sales..."
$sale1 = $salesList.Items.Add()
$sale1["ContentTypeId"] = $saleContentType.Id
$sale1.Update()
$sale1["Title"] = "Example Sale 1"
$sale1["Customer Name"] = "Wingtip Toys"
$regionValue = New-Object Microsoft.SharePoint.SPFieldLookupValue($region1["ID"], $region1["Title"])
$sale1["RegionLookup"] = $regionValue
$sale1["Amount"] = 150
$sale1.Update()
$sale2 = $salesList.Items.Add()
$sale2["ContentTypeId"] = $saleContentType.Id
$sale2.Update()
$sale2["Title"] = "Example Sale 2"
$sale2["Customer Name"] = "Adventure Works"
$regionValue = New-Object Microsoft.SharePoint.SPFieldLookupValue($region2["ID"], $region2["Title"])
$sale2["RegionLookup"] = $regionValue
$sale2["Amount"] = 200
$sale2.Update()
$sale3 = $salesList.Items.Add()
$sale3["ContentTypeId"] = $saleContentType.Id
$sale3.Update()
$sale3["Title"] = "Example Sale 3"
$sale3["Customer Name"] = "Adventure Works"
$regionValue = New-Object Microsoft.SharePoint.SPFieldLookupValue($region3["ID"], $region3["Title"])
$sale3["RegionLookup"] = $regionValue
$sale3["Amount"] = 120
$sale3.Update()

Write-Host
Write-Host "Creating Purchases site columns..." -ForegroundColor Yellow

$fieldXMLString = '<Field Type="Text"
Name="SupplierName"
Description="Use this column to specify the supplier for a purchase"
DisplayName="Supplier Name"
StaticName="SupplierName"
Group="Custom Columns"
Hidden="FALSE"
Required="FALSE"
Sealed="FALSE"
ShowInDisplayForm="TRUE"
ShowInEditForm="TRUE"
ShowInListSettings="TRUE"
ShowInNewForm="TRUE"></Field>'

Write-Host "Creating Supplier Name site column..."
$web.Fields.AddFieldAsXml($fieldXMLString)

Write-Host
Write-Host "Creating Purchase content type..." -ForegroundColor Yellow
$parentType = $web.AvailableContentTypes["Item"]
$purchaseContentType = New-Object Microsoft.SharePoint.SPContentType($parentType, $web.ContentTypes, "Purchase")
$purchaseContentType.Group = "Custom Content Types"
$web.ContentTypes.Add($purchaseContentType)

$transactionDescriptionColumn = $web.Fields.GetField("Transaction Description")
$transactionDescriptionFieldLink = New-Object Microsoft.SharePoint.SPFieldLink($transactionDescriptionColumn) 
$purchaseContentType.FieldLinks.Add($transactionDescriptionFieldLink) 
$purchaseContentType.Update()

$supplierNameColumn = $web.Fields.GetField("Supplier Name")
$supplierNameFieldLink = New-Object Microsoft.SharePoint.SPFieldLink($supplierNameColumn) 
$purchaseContentType.FieldLinks.Add($supplierNameFieldLink) 
$purchaseContentType.Update()

$regionsLookupColumn = $web.Fields.GetField("RegionLookup")
$regionsLookupFieldLink = New-Object Microsoft.SharePoint.SPFieldLink($regionsLookupColumn) 
$purchaseContentType.FieldLinks.Add($regionsLookupFieldLink)
$purchaseContentType.Update()

$amountColumn = $web.Fields.GetField("Amount")
$amountFieldLink = New-Object Microsoft.SharePoint.SPFieldLink($amountColumn) 
$purchaseContentType.FieldLinks.Add($amountFieldLink)
$purchaseContentType.Update() 

Write-Host
Write-Host "Creating Purchase Ledger list..." -ForegroundColor Yellow
$web.Lists.Add("PurchaseLedger", "Use this list to store purchases and their details", $customListTemplate)
$purchaseList = $web.Lists["PurchaseLedger"]
$purchaseList.Title = "Purchase Ledger"
$purchaseList.Update()

Write-Host "Adding the Purchase content type to the Purchase Ledger list..."
$purchaseList.ContentTypesEnabled = $true
$purchaseList.Update()
$purchaseList.ContentTypes.Add($purchaseContentType)
$purchaseList.Update()

Write-Host
Write-Host "Adding some purchases..."
$purchase1 = $purchaseList.Items.Add()
$purchase1["ContentTypeId"] = $purchaseContentType.Id
$purchase1.Update()
$purchase1["Title"] = "Example Purchase 1"
$purchase1["Supplier Name"] = "Wingtip Toys"
$regionValue = New-Object Microsoft.SharePoint.SPFieldLookupValue($region1["ID"], $region1["Title"])
$purchase1["RegionLookup"] = $regionValue
$purchase1["Amount"] = 10
$purchase1.Update()
$purchase2 = $purchaseList.Items.Add()
$purchase2["ContentTypeId"] = $purchaseContentType.Id
$purchase2.Update()
$purchase2["Title"] = "Example Purchase 2"
$purchase2["Supplier Name"] = "Wingtip Toys"
$regionValue = New-Object Microsoft.SharePoint.SPFieldLookupValue($region2["ID"], $region2["Title"])
$purchase2["RegionLookup"] = $regionValue
$purchase2["Amount"] = 25
$purchase2.Update()
$purchase3 = $purchaseList.Items.Add()
$purchase3["ContentTypeId"] = $purchaseContentType.Id
$purchase3.Update()
$purchase3["Title"] = "Example Purchase 3"
$purchase3["Supplier Name"] = "Adventure Works"
$regionValue = New-Object Microsoft.SharePoint.SPFieldLookupValue($region1["ID"], $region1["Title"])
$purchase3["RegionLookup"] = $regionValue
$purchase3["Amount"] = 55
$purchase3.Update()

#Dispose of all objects
Stop-SPAssignment -Global

Write-Host
Write-Host "Finished." -ForegroundColor Green
Write-Host
Write-Host "Please check the output for errors and then press any key to continue..." -ForegroundColor White
$x = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
# SIG # Begin signature block
# MIIavQYJKoZIhvcNAQcCoIIarjCCGqoCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUWtdIPxkGTmM9VL3zt6BuEL50
# GTGgghWCMIIEwzCCA6ugAwIBAgITMwAAADPlJ4ajDkoqgAAAAAAAMzANBgkqhkiG
# 9w0BAQUFADB3MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4G
# A1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSEw
# HwYDVQQDExhNaWNyb3NvZnQgVGltZS1TdGFtcCBQQ0EwHhcNMTMwMzI3MjAwODIz
# WhcNMTQwNjI3MjAwODIzWjCBszELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hp
# bmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jw
# b3JhdGlvbjENMAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNpcGhlciBEU0UgRVNO
# OkY1MjgtMzc3Ny04QTc2MSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBT
# ZXJ2aWNlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAyt7KGQ8fllaC
# X9hCMtQIbbadwMLtfDirWDOta4FQuIghCl2vly2QWsfDLrJM1GN0WP3fxYlU0AvM
# /ZyEEXmsoyEibTPgrt4lQEWSTg1jCCuLN91PB2rcKs8QWo9XXZ09+hdjAsZwPrsi
# 7Vux9zK65HG8ef/4y+lXP3R75vJ9fFdYL6zSDqjZiNlAHzoiQeIJJgKgzOUlzoxn
# g99G+IVNw9pmHsdzfju0dhempaCgdFWo5WAYQWI4x2VGqwQWZlbq+abLQs9dVGQv
# gfjPOAAPEGvhgy6NPkjsSVZK7Jpp9MsPEPsHNEpibAGNbscghMpc0WOZHo5d7A+l
# Fkiqa94hLwIDAQABo4IBCTCCAQUwHQYDVR0OBBYEFABYGz7txfEGk74xPTa0rAtd
# MvCBMB8GA1UdIwQYMBaAFCM0+NlSRnAK7UD7dvuzK7DDNbMPMFQGA1UdHwRNMEsw
# SaBHoEWGQ2h0dHA6Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3Rz
# L01pY3Jvc29mdFRpbWVTdGFtcFBDQS5jcmwwWAYIKwYBBQUHAQEETDBKMEgGCCsG
# AQUFBzAChjxodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2NlcnRzL01pY3Jv
# c29mdFRpbWVTdGFtcFBDQS5jcnQwEwYDVR0lBAwwCgYIKwYBBQUHAwgwDQYJKoZI
# hvcNAQEFBQADggEBAAL/44wD6u9+OLm5fJ87UoOk+iM41AO4alm16uBviAP0b1Fq
# lTp1hegc3AfFTp0bqM4kRxQkTzV3sZy8J3uPXU/8BouXl/kpm/dAHVKBjnZIA37y
# mxe3rtlbIpFjOzJfNfvGkTzM7w6ZgD4GkTgTegxMvjPbv+2tQcZ8GyR8E9wK/EuK
# IAUdCYmROQdOIU7ebHxwu6vxII74mHhg3IuUz2W+lpAPoJyE7Vy1fEGgYS29Q2dl
# GiqC1KeKWfcy46PnxY2yIruSKNiwjFOPaEdHodgBsPFhFcQXoS3jOmxPb6897t4p
# sETLw5JnugDOD44R79ECgjFJlJidUUh4rR3WQLYwggTsMIID1KADAgECAhMzAAAA
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
# HAYKKwYBBAGCNwIBCzEOMAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFD8b
# gH7SUb5hlWJY+gi02et43nzgMF4GCisGAQQBgjcCAQwxUDBOoCaAJABNAGkAYwBy
# AG8AcwBvAGYAdAAgAEwAZQBhAHIAbgBpAG4AZ6EkgCJodHRwOi8vd3d3Lm1pY3Jv
# c29mdC5jb20vbGVhcm5pbmcgMA0GCSqGSIb3DQEBAQUABIIBAHCYq/ZVmG69DrYt
# l2LiN6pOylGOjYgQntv3kaqjeR2Pj1eIfVmFbKz33ALESWHjFni46ig997WBLl5l
# 0Ms8VYJWTHig665QusdAvFlSETrvX+MKXQISCij0sO8W9QFHlONgFWBWiqR0ADvf
# RX74112pC4nJv8Yv4D/SnhWg0tNh6Vj1xX5WxI3QCuvjVFosidE9NZA1YTOcU/9y
# 1YBNqRI7tHLMEAdg92FMyVhLesiV6ddpkgMvrRGxjUnJfwQ+NHXC9dYfGeckDRRg
# 36yECFNeoNzZ5Xps70MSEZTstkJQGVoVTUmajIakdaeyuqxRYVbeezLzo9yR5myd
# kl21rzmhggIoMIICJAYJKoZIhvcNAQkGMYICFTCCAhECAQEwgY4wdzELMAkGA1UE
# BhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAc
# BgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEhMB8GA1UEAxMYTWljcm9zb2Z0
# IFRpbWUtU3RhbXAgUENBAhMzAAAAM+UnhqMOSiqAAAAAAAAzMAkGBSsOAwIaBQCg
# XTAYBgkqhkiG9w0BCQMxCwYJKoZIhvcNAQcBMBwGCSqGSIb3DQEJBTEPFw0xMzA3
# MDExODAwNDFaMCMGCSqGSIb3DQEJBDEWBBToOq4P06PxBP+Ps4KGyclf90HgDTAN
# BgkqhkiG9w0BAQUFAASCAQAN6UdOL/krCifg/q1StBGl4qzUIbHpvspTZeVi1gQ7
# NJ0mm4XKvWMZ2QcsxGzdrSMsrEenV+UykC7fuzlDulu9mV2Gycd2abEARDR+vczk
# t+eQ8owkxOqii2Q/AQqN2aMjs1tjl8xVx/ZnK8QSb3Tk5AKixS/inbGAxafeh6dZ
# zYjdY3/Tb+LakZVv0oynHooFS/SR3V/dZXjrsGCMCs+QAapGfdLmEQ+Jht4e0aho
# dHJO3nTJixnmWZXBAH6Pkb/K1Hmf5hrqIv3RGo1wtKkuD2uKYQLzOw6Pg0QV9dSB
# w+czme8BdLnm8ejqQ0XnnWRY4QaYRXg+n317mh5xaUyt
# SIG # End signature block
