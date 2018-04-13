import program from 'commander';
import inquire from 'inquirer';
import { Commands } from './commands';

const questions = [
    {
        type: 'input',
        name: 'filename',
        message: 'Enter model names ...'
    }
];

program
    .command('read')
    .alias('r')
    .description('read file')
    .action(() => {
        inquire.prompt(questions).then((answers) => {
            let cmd = new Commands();
            Promise.all([cmd.read(answers.filename)]);
        });
    });

program
    .command('diff')
    .alias('d')
    .description('diff file')
    .action(() => {
        inquire.prompt(questions).then((answers) => {
            let cmd = new Commands();
            Promise.all([cmd.diff(answers.filename)]);
        });
    });

program
    .command('watch')
    .alias('w')
    .description('watch file')
    .action(() => {
        let cmd = new Commands();
        cmd.watch();
    });

program
    .version('0.0.1')
    .description('Contact management system');

program.parse(process.argv);
