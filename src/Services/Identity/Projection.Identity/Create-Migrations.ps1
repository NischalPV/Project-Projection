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
        [string]$dbContextName = "ApplicationDbContext",
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
        [string]$dbContextName = "ApplicationDbContext"
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
Add-Migration -dbContextName "ApplicationDbContext" -migrationName "InitialSql" -outputDir "Data/Migrations";
Add-Migration -dbContextName "ConfigurationDbContext" -migrationName "InitialSql" -outputDir "Data/Migrations/IdentityServer/ConfigurationDb";
Add-Migration -dbContextName "PersistedGrantDbContext" -migrationName "InitialSql" -outputDir "Data/Migrations/IdentityServer/PersistedGrantDb";

# Update database
Update-Database -dbContextName "ApplicationDbContext";
Update-Database -dbContextName "ConfigurationDbContext";
Update-Database -dbContextName "PersistedGrantDbContext";

