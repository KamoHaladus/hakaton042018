import { ModelDeclaration } from "./modelDeclaration";

const { exec } = require('child_process');
export class Commands {
    public sync = () => console.log('not implemented');

    public diff = () => console.log('not implemented');

    public read = (fileName: string): ModelDeclaration[] => {
        let changes: ModelDeclaration[] = [{
            name: 'ActivityViewModel',
            members: [
                { name: "PropertySubTypeId", type: { name: "string" } },
                { name: "PropertyTypeId", type: { name: "number" } }
            ]
        }];

        return changes;
    };

    private invoke = ():  => {
        exec("cat package.json | wc -l", () => {

        });
    }
}