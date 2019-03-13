cd src/platformy/technologiczne
rm *.class
javac *.java
move *.class ../../../out/production/server/platformy/technologiczne
cd ../../..
cd out/production/server
java platformy.technologiczne.Server 1