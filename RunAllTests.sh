: '

HOW TO RUN

1.  Ensure PowerShell Core (pwsh) is Installed:
    Make sure you have PowerShell Core installed on your system. You can download it from the 

2.  Navigate to Script Directory:
    Open a terminal and navigate to the directory containing the RunAllTests.sh script.

3.  Make Script Executable:
    Run the following command to make the script executable:
    
    > chmod +x RunAllTests.sh

4.  Run the Bash Script: 
    Execute the script by running the following command:

    > ./RunAllTests.sh

5.  Go to the test project directory to see the tests results in a log file.
    - Account.API.UnitTests.log
    - Transaction.API.UnitTests.log
    - User.API.UnitTests.log

'

accountApiTestPath="./tests/Account.API.UnitTests"
transactionApiTestPath="./tests/Transaction.API.UnitTests"
userApiTestPath="./tests/User.API.UnitTests"

nohup dotnet test "$accountApiTestPath" > "$accountApiTestPath.log" 2>&1 &
nohup dotnet test "$transactionApiTestPath" > "$transactionApiTestPath.log" 2>&1 &
nohup dotnet test "$userApiTestPath" > "$userApiTestPath.log" 2>&1
