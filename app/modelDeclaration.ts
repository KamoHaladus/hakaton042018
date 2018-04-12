export type ModelDeclaration = {
    name: string;
    members: MemberDeclaration[];
}

export type MemberDeclaration = {
    name: string,
    type: TypeDeclaration
}

export type TypeDeclaration = {
    name: string,
    isArray?: boolean;
}