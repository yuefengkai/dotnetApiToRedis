# encoding:utf-8
import itchat
import sys
from datetime import datetime
from apscheduler.schedulers.background import BackgroundScheduler

import requests

import logging

logging.basicConfig(

level=logging.DEBUG,

format='%(asctime)s %(filename)s[line:%(lineno)d] %(levelname)s %(message)s',

datafmt='%a, %d %b %Y %H:%M:%S',

filename='./aaa.txt',

filemode='a')

reload(sys)  

sys.setdefaultencoding('utf-8')

#发送群聊消息
def sentChatRoomsMsg(name, context):
    itchat.get_chatrooms(update=True)
    iRoom = itchat.search_chatrooms(name)
    for room in iRoom:
        if room['NickName'] == name:
            userName = room['UserName']
            break
   
    print("发送时间：" + datetime.now().strftime("%Y-%m-%d %H:%M:%S") + "\n"
                                                                   "发送到：" + name + "\n"
                                                                                   "发送内容：" + context + "\n")
    print("*********************************************************************************")

    itchat.send_msg(context, userName)

@itchat.msg_register(itchat.content.TEXT)
def print_content(msg):
    s = msg['Text'].encode("utf-8")

    print(s)
    print(msg.User.NickName)

    if(s=="send"):
        sentChatRoomsMsg('阳光喔 | 深圳员工群2',datetime.now().strftime("%Y-%m-%d %H:%M:%S") + "闭上眼睛，安静内心告诉自己:别人想什么，我们控制不了；别人做什么，我们也强求不了。唯一可以做的 就是尽心尽力做好自己的事，走自己的路，按自己的原则，好好生活。即使有人亏待了你，时间也不会亏待你，人生更加不会亏待你。")


jobCount = 0
scheduler = BackgroundScheduler()
taskJobs = []

def getHttpJobs():
    global jobCount

    print(111)

    #请求地址
    url = "http://gaozengzhi.cn:32770/api/chatmsg/all"
    #发送get请求
    r = requests.get(url)
    #获取返回的json数据
    print(r.json())  

    for job in r.json():
        

        if job['id'] in taskJobs:
            continue
        
        
        taskJobs.append(job['id'])

       
        chatRoom = job['chatRoom']
        context = job['content']
        date_value =  datetime.strptime(job['dateTime'], "%Y-%m-%d %H:%M:%S")
        if(datetime.now()>date_value):
           print(date_value.strftime("%Y-%m-%d %H:%M:%S")+ '时间已过')
           continue

        scheduler.add_job(sentChatRoomsMsg, 'date', run_date=date_value, kwargs={"name": chatRoom, "context": context})
        
        jobCount = jobCount + 1  
        
        print('添加任务成功-》'+job['dateTime']+",chatRoom:"+chatRoom+",context:"+job['content'])

def addJobs():
    global jobCount

    try:  
        getHttpJobs()
    except Exception,e:  
        print Exception,":",e

    scheduler.add_job(getHttpJobs, 'interval', minutes=1)

  
    print("jobs start...")

def loginCallback():
    
    addJobs() 
    scheduler.print_jobs()
    scheduler.start()   
    print("***登录成功***")
    
  

def exitCallback():
    itchat.logout()
    print("***已退出***")


if __name__ == "__main__":
    itchat.auto_login(enableCmdQR=2,loginCallback=loginCallback, exitCallback=exitCallback)
#itchat.auto_login(True)
    print datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    itchat.run()



