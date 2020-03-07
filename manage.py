import os

import click

from App import create_app

env = os.environ.get('FLASK_ENV')
app = create_app(env)


# manager = Manager(app)
# manager.add_command('db', MigrateCommand)
# formal version of start cmd with parameters

@click.command()
@click.option('--mode', default="develop", type=click.Choice(["develop", "produce"]), help="--develop/--produce")
def run(mode):
    if mode == "develop":
        app.run(host='127.0.0.1', debug=True, port=8080)
    else:
        app.run()


if __name__ == '__main__':
    run()
