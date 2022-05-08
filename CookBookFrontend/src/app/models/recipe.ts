export class Recipe {
    constructor(
        public id: Number,
        public title: string,
        public shortDescription: string,
        public preparingTime: Number,
        public tags: string[],
        public likesCount: Number,
        public favoritesCount: Number) { }
}
