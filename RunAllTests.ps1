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

5.  See the logs in the terminal.

#>

$accountApiTestPath = ".\test\Account.API.UnitTests"
$transactionApiTestPath = ".\test\Transaction.API.UnitTests"
$userApiTestPath = ".\test\User.API.UnitTests"

Start-Process "dotnet" -ArgumentList "test" -WorkingDirectory $accountApiTestPath -PassThru | Wait-Process
Start-Process "dotnet" -ArgumentList "test" -WorkingDirectory $transactionApiTestPath -PassThru | Wait-Process
Start-Process "dotnet" -ArgumentList "test" -WorkingDirectory $userApiTestPath -PassThru | Wait-Process
