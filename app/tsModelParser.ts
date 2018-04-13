import ts from 'typescript';
import { MemberDeclaration, ModelDeclaration, TypeDeclaration } from './types';
import { openTypescriptFile, SyntaxKindToStringTypeMapping } from './tsHelpers';

export async function parseTypescriptModel(path: string): Promise<ModelDeclaration> {
    const src = await openTypescriptFile(path);
    const members: MemberDeclaration[] = [];
    let modelName: string;

    src.forEachChild(visitNode);

    return {
        name: modelName!,
        members
    };

    function visitNode(node: ts.Node) {
        if (node.kind == ts.SyntaxKind.ClassDeclaration) {
            const classDecl = (node as ts.ClassDeclaration);
            modelName = classDecl.name!.text;
            members.push(...extractClassMembers(classDecl));
        } else if (node.kind == ts.SyntaxKind.InterfaceDeclaration) {
            const interfaceDecl = (node as ts.InterfaceDeclaration);
            modelName = interfaceDecl.name!.text;
            members.push(...extractInterfaceMembers(interfaceDecl));
        }

        node.forEachChild(visitNode);
    }

    function extractClassMembers(classDecl: ts.ClassDeclaration) {
        const classMembers = classDecl.members
            .filter(m => m.kind == ts.SyntaxKind.PropertyDeclaration)
            .filter(m => {
                const prop = m as ts.PropertyDeclaration;

                return !prop.initializer || prop.initializer.kind != ts.SyntaxKind.ArrowFunction;
            }) as ts.PropertyDeclaration[];

        return classMembers.map(getMember);
    }

    function extractInterfaceMembers(classDecl: ts.InterfaceDeclaration) {
        const classMembers = classDecl.members
            .filter(m => m.kind == ts.SyntaxKind.PropertySignature) as ts.PropertySignature[];

        return classMembers.map(getMember);
    }
}

function getMember(prop: ts.PropertyDeclaration | ts.PropertySignature): MemberDeclaration {
    let type: TypeDeclaration = {
        name: 'Unknown'
    };

    const name = (prop.name as ts.Identifier).text;

    if (prop.type) {
        if (prop.type.kind == ts.SyntaxKind.ArrayType) {
            const arrayNode = prop.type as ts.ArrayTypeNode;

            type.name = getType(arrayNode.elementType) || 'Unknown2';
            type.isArray = true;
        } else {
            type.name = getType(prop.type) || 'Unknown2';
        }
    } else {
        debugger;
    }

    return { name, type }
}

function getType(node: ts.TypeNode) {
    const mapping = SyntaxKindToStringTypeMapping.get(node.kind);

    if (mapping) {
        return mapping;
    } else if (node.kind == ts.SyntaxKind.TypeReference) {
        const typeNode = node as ts.TypeReferenceNode;
        if (typeNode.typeName.kind == ts.SyntaxKind.Identifier) {
            return (typeNode.typeName as ts.Identifier).text;
        } else if (typeNode.typeName.kind == ts.SyntaxKind.QualifiedName) {
            // TODO: advertisingPortals
        }
    } else {
        console.log('!!!!');
        debugger;
    }
}
