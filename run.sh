#! /bin/bash 

echo '******************************************************************************'
docker-compose build
echo '*************docker 编译成功*************'

docker login -u=yuefengkai  hub.tencentyun.com
echo '*************登录成功*************'

docker tag dotnetapitoredis hub.tencentyun.com/yuefengkai/dotnetapitoredis:latest
echo '*************标记成功dotnetapitoredis*************'

docker push hub.tencentyun.com/yuefengkai/dotnetapitoredis:latest
echo '*************推送成功dotnetapitoredis*************'

docker tag itchatmsg hub.tencentyun.com/yuefengkai/itchatmsg:latest
echo '*************标记成功itchatmsg*************'

docker push hub.tencentyun.com/yuefengkai/itchatmsg:latest
echo '*************推送成功itchatmsg*************'

docker rmi $(docker images -f "dangling=true" -q) 

echo '******************************************************************************'