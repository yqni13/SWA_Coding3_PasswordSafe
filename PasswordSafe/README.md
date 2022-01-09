### edited by: Lukas Varga, if20b167

------

## SWA_Coding#3_PasswordSafe

Software Architecture Project #3 to create a program as PasswordSafe with all the necessary patterns included

### changes: 

- **no access to master password**
  *as master password is set the password is hashed with build-in function => password can't be read in plaintext - to compare the input from console is hashed too and both hash values are compared on equality to unlock application*
- **check new password for equality**
  *added new class 'CheckPwEquality' to ask for the password a second time and check on equality - returns boolean and is used in main program for setting the master password and new regular passwords*
- **flexible configuration of password path**
  *via config file 'App.config' the path for regular and master password can be set and name of master password too - default path is writen in comment and it can be used via absolute and relative path*
  
- **multiple encryption methods**
  *added new class EncodingMethodsCollection which includes the different en/decryption functions for available methods - for presentation I implemented TripleDES Method. In class CipherFacility is a switch which calls integer to choose encoding method from config file*
- **database / single pw-file**
  *task not done*
