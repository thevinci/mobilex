#!/bin/bash
for arg in "$@"
do
  index=$(echo $arg | cut -f1 -d=)
  val=$(echo $arg | cut -f2 -d=)
case $index in
  t) t=$val;;
  j) j=$val;;
  r) r=$val;;
*)
esac
done

mkdir -p ./cdn/teams/$t/jobs/$j/runs/$r/
node scripts/appium/run.js -t $t -j $j -r $r > ./cdn/teams/$t/jobs/$j/runs/$r/logs.txt
node scripts/appium/saveToDb.js -t $t -j $j -r $r

echo "APPIUM DONE -teamId $t -jobId $j -runId $r"