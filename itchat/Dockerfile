FROM python:2.7
ADD . /code
WORKDIR /code

#设置时区
RUN /bin/cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo 'Asia/Shanghai' >/etc/timezone

RUN pwd&&ls

RUN pip install -r requirements.txt

ENV PYTHONUNBUFFERED=0

ENTRYPOINT ["python", "itchatMsg.py"]