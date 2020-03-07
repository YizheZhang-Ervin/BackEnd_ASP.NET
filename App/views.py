import datetime

from flask import Blueprint, request, render_template

from core.goldanalysis import plot_diy, getorigintime, plot_price_table, gettime, plot_price_trend, plot_price_trend_l, \
    plot_animation, plot_3D

blue = Blueprint('blue', __name__)


def init_blue(app):
    app.register_blueprint(blue)

# index.html
@blue.route('/', methods=['GET', 'POST'])
def index():
    if request.method == 'GET':
        return render_template('index.html')


# dashboard.html
@blue.route('/dashboard/', methods=['GET', 'POST'])
def dashboard():
    if request.method == 'POST':
        date001 = request.form.get('date001', '')
        name001 = request.form.get('name001', '')
        data001 = request.form.getlist('data001')
        try:
            diychart = plot_diy(date001, name001, *data001)
            return render_template('dashboard.html', time='', type='', name='', diychart=diychart)
        except Exception:
            return render_template('dashboard.html', time='', type='', name='', diychart='')
    if request.method == 'GET':
        currenttime = getorigintime()
        recordtime = datetime.datetime.strptime('2019-12-16', "%Y-%m-%d")
        name = ''
        try:
            action = request.args.get('time', '')
            action2 = request.args.get('type', '')
        except Exception:
            action, action2 = '', ''

        if action == 'days' or action == 'tables':
            # 1 week table
            aweek_table = (currenttime - datetime.timedelta(days=6)).strftime('%Y-%m-%d')
            time, name = plot_price_table(aweek_table, gettime())
        elif action == '1week':
            # 1 week
            aweek = (currenttime - datetime.timedelta(days=7)).strftime('%Y-%m-%d')
            time, name = plot_price_trend(aweek, '1week')
        elif action == '2weeks':
            # 2 weeks
            twoweeks = (currenttime - datetime.timedelta(days=14)).strftime('%Y-%m-%d')
            time, name = plot_price_trend(twoweeks, '2weeks')
        elif action == '3weeks':
            # 3 weeks
            threeweeks = (currenttime - datetime.timedelta(days=21)).strftime('%Y-%m-%d')
            time, name = plot_price_trend(threeweeks, '3weeks')
        elif action == '1month':
            # 1 month
            onemonth = (currenttime - datetime.timedelta(days=30)).strftime('%Y-%m-%d')
            time, name = plot_price_trend(onemonth, '1month')
        elif action == '2months':
            # 2 months
            twomonths = (currenttime - datetime.timedelta(days=60)).strftime('%Y-%m-%d')
            time, name = plot_price_trend(twomonths, '2months')
        elif action == '3months':
            # 3 months
            threemonths = (currenttime - datetime.timedelta(days=90)).strftime('%Y-%m-%d')
            time, name = plot_price_trend(threemonths, '3months')
        elif action == '6months':
            # 6 months
            sixmonths = (recordtime - datetime.timedelta(days=180)).strftime('%Y-%m-%d')
            time, name = plot_price_trend_l(sixmonths, '6months')
        elif action == '1year':
            # 1 year
            oneyear = (recordtime - datetime.timedelta(days=360)).strftime('%Y-%m-%d')
            time, name = plot_price_trend_l(oneyear, '1year')
        elif action == '2years':
            # 2 years
            twoyears = (recordtime - datetime.timedelta(days=720)).strftime('%Y-%m-%d')
            time, name = plot_price_trend_l(twoyears, '2years')
        elif action == '3years':
            # 3 years
            threeyears = (recordtime - datetime.timedelta(days=1080)).strftime('%Y-%m-%d')
            time, name = plot_price_trend_l(threeyears, '3years')
        elif action == '5years':
            # 5 years
            fiveyears = (recordtime - datetime.timedelta(days=1800)).strftime('%Y-%m-%d')
            time, name = plot_price_trend_l(fiveyears, '5years')
        elif action == '10years':
            # 10 years
            tenyears = (recordtime - datetime.timedelta(days=3600)).strftime('%Y-%m-%d')
            time, name = plot_price_trend_l(tenyears, '10years')
        elif action == '12years':
            # 12 years
            twelveyears = (recordtime - datetime.timedelta(days=4320)).strftime('%Y-%m-%d')
            time, name = plot_price_trend_l(twelveyears, '12years')
        elif action == '':
            time = ''

        if action2 == '1line-animation':
            typepic, name = plot_animation('All time animation')
        elif action2 == '3d':
            time, name = plot_3D('All time 3D')
            typepic = ''
        elif action2 == 'diy':
            typepic = ''
            name = 'Please select Date/Data which you need to analyze'
        else:
            typepic = ''

        # Transfer parameters
        return render_template('dashboard.html', time=time, typepic=typepic, name=name, diychart='')


# tools.html
@blue.route('/tools/', methods=['GET', 'POST'])
def tools():
    if request.method == 'GET':
        return render_template('tools.html')


# error handler
# @app.errorhandler(404)
# def page_not_found(error):
#     # use template
#     # return render_template('page_not_found.html'), 404
#     # gain response and change
#     resp = make_response(render_template('error.html'), 404)
#     resp.headers['X-Something'] = 'A value'
#     return resp
