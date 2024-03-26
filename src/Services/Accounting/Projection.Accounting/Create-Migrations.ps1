# Build project function
function Build-Project {
    Write-Output "Building project";

    $res = dotnet build

    if ($null -ne $res) {
        $res
    }
    else {
        "Dotnet build failed"
    }
}


# Create migrations function
function Add-Migration {

    param(
        [string]$dbContextName = "AccountingDbContext",
        [string]$migrationName = "Initial",
        [string]$outputDir = "Data/Migrations"
    )

    Write-Output "Creating migration for $dbContextName";

    $res = dotnet ef migrations add $migrationName --context $dbContextName --output-dir $outputDir

    if ($null -ne $res) {
        $res
    }
    else {
        "Dotnet ef migrations add failed for $dbContextName"
    }
}

# Update database function
function Update-Database {

    param(
        [string]$dbContextName = "AccountingDbContext"
    )

    Write-Output "Updating database for $dbContextName";

    $res = dotnet ef database update --context $dbContextName

    if ($null -ne $res) {
        $res
    }
    else {
        "Dotnet ef database update failed"
    }
}

# Build project before creating migrations
Build-Project

# update dotnet ef tool to latest available version
dotnet tool update --global dotnet-ef

# Create migrations
Add-Migration -dbContextName "AccountingDbContext" -migrationName "Initial" -outputDir "Data/Migrations/AccountingDb";
Add-Migration -dbContextName "IntegrationEventLogContext" -migrationName "Initial" -outputDir "Data/Migrations/IntegrationEventLog";

# Update database
Update-Database -dbContextName "AccountingDbContext";
Update-Database -dbContextName "IntegrationEventLogContext";