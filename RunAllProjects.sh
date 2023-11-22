: '

HOW TO RUN

1.  Ensure PowerShell Core (pwsh) is Installed:
    Make sure you have PowerShell Core installed on your system. You can download it from the 

2.  Navigate to Script Directory:
    Open a terminal and navigate to the directory containing the RunAllProjects.sh script.

3.  Make Script Executable:
    Run the following command to make the script executable:
    
    > chmod +x RunAllProjects.sh

4.  Run the Bash Script: 
    Execute the script by running the following command:

    > ./RunAllProjects.sh

5.  Go to your browser and check the service swaggers.

'

accountApiPath="./src/Account/Account.API"
transactionApiPath="./src/Transaction/Transaction.API"
userApiPath="./src/User/User.API"
webBffApiPath="./src/BFF/Web.BFF.API"

dotnet run --project "$accountApiPath" &
dotnet run --project "$transactionApiPath" &
dotnet run --project "$userApiPath" &
dotnet run --project "$webBffApiPath" &

wait
