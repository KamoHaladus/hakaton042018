import program from 'commander';
import inquire from 'inquirer';
import { Commands } from './commands';
import { parseTypescriptModel } from './tsModelParser';

const questions = [
    {
        type: 'input',
        name: 'filename',
        message: 'Enter filename ...'
    }
];

program
    .command('read') // No need of specifying arguments here
    .alias('r')
    .description('read file')
    .action(() => {
        inquire.prompt(questions).then((answers) => {
            let cmd = new Commands();
            Promise.all([cmd.read(answers.filename)]);
        });
    });

program
    .version('0.0.1')
    .description('Contact management system');

program.parse(process.argv);
