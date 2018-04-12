const program = require('commander');
const inquire = require('inquirer');
const { sync, diff, read } = require('./command');

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
        inquire.prompt(questions).then(answers => {
            console.log(answers);
            console.log(read);
            read(answers.filename);
        });
    });

program
    .version('0.0.1')
    .description('Contact management system');

program.parse(process.argv);
