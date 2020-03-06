# NIA7

## Run
    Method 1) path> set FLASK_APP=hello.py
              path> flask run
    
    Method 2) path> python -m flask run
    
    Method 3) path>python -m flask run --host=0.0.0.0 (all users)
    
## Deploy
    Gunicorn Common 1) gunicorn -w 4 -b 127.0.0.1:4000 myproject:app
    
    Gunicorn Factory 2) gunicorn "myproject:create_app()"
    
    uWSGI 1) uwsgi --http 127.0.0.1:5000 --module myproject:app
    
    twistd 1) twistd -n web --port tcp:8080 --wsgi myproject.app
    
    Gevent 1) 