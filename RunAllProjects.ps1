<#

HOW TO RUN

1.  Ensure PowerShell is Installed:
    Make sure you have PowerShell installed on your system. If not, you can download it from PowerShell GitHub Releases.
   
2.  Set Execution Policy:
    Open a PowerShell window as an administrator and run the following command to set the execution policy (if needed):
   
    > Set-ExecutionPolicy RemoteSigned

3.  Navigate to Script Directory:
    Open a terminal and navigate to the directory containing the RunAllProjects.ps1 script.

4.  Run the PowerShell Script:
    Execute the script by running the following command:
    
    > ./RunAllProjects.ps1

5.  Go to your browser and check the service swaggers.

#>

$accountApiPath = ".\src\Account\Account.API"
$transactionApiPath = ".\src\Transaction\Transaction.API"
$userApiPath = ".\src\User\User.API"
$webBffApiPath = ".\src\BFF\Web.BFF.API"

Start-Process "dotnet" -ArgumentList "run" -WorkingDirectory $accountApiPath -PassThru | Wait-Process
Start-Process "dotnet" -ArgumentList "run" -WorkingDirectory $transactionApiPath -PassThru | Wait-Process
Start-Process "dotnet" -ArgumentList "run" -WorkingDirectory $userApiPath -PassThru | Wait-Process
Start-Process "dotnet" -ArgumentList "run" -WorkingDirectory $webBffApiPath -PassThru | Wait-Process
