import { parseTypescriptModel } from './tsModelParser';

const classPath = "";
const interfacePath = "";

parseTypescriptModel(classPath).then(model => {
    console.log(model);
    // console.log(members.filter(x => x.type.name.startsWith('Unknown')));
});

parseTypescriptModel(interfacePath).then(model=> {
    console.log(model);
    // console.log(members.filter(x => x.type.name.startsWith('Unknown')));
});
