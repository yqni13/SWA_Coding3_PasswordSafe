### edited by: Lukas Varga, if20b167

------

## SWA_Coding#3_PasswordSafe

Software Architecture Project #3 to create a program as PasswordSafe with all the necessary patterns included

### changes: 

- **no access to master password**
  *as master password is set the password is hashed with build-in function => password can't be read in plain text - to compare the input from console (Console.ReadLine() from unlock action [1]) is hashed too and both hash values are compared on equality to unlock application*
- **check new password for equality**
  *added new class 'CheckPwEquality.cs' to ask for the password a second time and check on equality - returns boolean and is used in main program for setting the master password and new regular passwords*
- **flexible configuration of password path**
  *via config file 'App.config' (located at same path as classes) the path for regular and master password can be set and name of master password too - default path is written in comment and it can be used via absolute or relative path => used in 'Program.cs', 'CipherFacility.cs' and 'MasterPasswordRepository.cs'*
- **multiple encryption methods**
  *added new class 'EncodingMethodsCollection.cs' which includes different en/decryption functions for available methods - for presentation I added the TripleDES Method => adapted 'CipherFacility.cs' with a switch which calls an integer to choose encoding method by value from config file*
  
- **database / single pw-file**
  *task not done*
