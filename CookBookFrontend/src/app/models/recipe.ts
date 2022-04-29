export class Recipe {
    constructor(
        public id: Number,
        public title: String,
        public shortDescription: String,
        public preparingTime : Number,
        public tags : String[],
        public likesCount : Number,
        public favoritesCount : Number) {}
}
