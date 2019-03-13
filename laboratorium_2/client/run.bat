cd src/sample
rm *.class
javac *.java
move *.class ../../out/production/client/sample
cd ../..
cd out/production/client
java sample.Main