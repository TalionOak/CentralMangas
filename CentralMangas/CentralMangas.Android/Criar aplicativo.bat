@ECHO OFF
TITLE Criando AAB
ECHO Criando AAB... Aguarde...
msbuild -restore CentralMangas.Android.csproj -t:SignAndroidPackage -p:Configuration=Release -p:AndroidKeyStore=True -p:AndroidSigningKeyStore=TalionOak/TalionOak.keystore -p:AndroidSigningStorePass=senha123 -p:AndroidSigningKeyAlias=TalionOak -p:AndroidSigningKeyPass=senha123
PAUSE
