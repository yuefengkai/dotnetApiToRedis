#! /bin/bash 

echo '******************************************************************************'
docker-compose build
echo '*************docker 编译成功*************'

docker login -u=yuefengkai -p 19901230 hub.tencentyun.com
echo '*************登录成功*************'

docker tag dotnetapitoredis hub.tencentyun.com/yuefengkai/dotnetapitoredis:latest
echo '*************标记成功*************'

docker push hub.tencentyun.com/yuefengkai/dotnetapitoredis:latest
echo '*************推送成功*************'

echo '******************************************************************************'