## Cooking Minigame

Here, we brainstorm the cooking mini-game in typescript psuedo-coded

```typescript

type Ingredient = {
    name: string; // apple, pear, avocado, toast, candy
    price: number;
    taste:  {
        sweet: number;
        spicy: number;
        sour: number;
        salty: number;
        bitter: number;
        umami: number;
        metallic: number;
        horseRadish: number;    
    },
    smell: {
        fruity: number;
        floral: number;
        smoke: number;
        savory:  number;
        rot: number;
    },
    touch: {
        solidity: number; // >0.7 is solid, between 0.3 <=> 0.7 is  liquid, and <0.3 is gas
        smoothness: number;
        crispiness: number;
        stickiness: number;
        temperature: number;
        moisture: number;
    },
    physics: {
        weight: number;
        volume: number;
        tensileStrength: number;
        shearStrength: number;
    },
    special: {
        hallucingenic: number;
        luck: number;
        fiber: number;
        fat: number;
        poison: number;
    },
}

interface FinishedDish extends Ingredient {
    category: 'dessert' | 'soup' | 'beverage' | 'appetizer' |  'entree' | 'toy' | 'building  material'
    isFinished: true;
    appeal: {
        health: number; //  safety needs
        luxury: number;  // esteem needs
        delicious: number; // physiological needs
        wow: number; //  actualization  needs
        joy: number; // love and belonging
    };
}

type FinishFn = (ingredient: Ingredient) => FinishedDish;
type MappingFn = (ingredient: Ingredient) => Ingredient;
type ReducingFn = (primA: Ingredient, primB: Ingredient) => Ingredient;
type SplitFn = (ingredient: Ingredient) =>  [Ingredient, Ingredient];
const SoupFinishFn: FinishFn  = i =>  {
    return {
        ...i,
        category: 'soup',
        isFinished: true,
        appeal: {
            delicious:  f(i.taste, i.touch.temperature, ...etc)
        }
    }
}
function isBuildingMaterial(ingredient:  Ingredient) {
    return ingredient.special.poison  > 0.1;
}

const dicing: MappingFn = (ingredient) => {
    if (ingredient.touch.solidity <= 0.7)  {
        return ingredient;
    }
    return  {
        taste: ingredient.taste,
        smell: mapV(ingredient.smell, atom =>  atom * 1.2),
        physics: {
            weight: ingredienti.physics.weight,
            volume: ingredienti.physics.volume,
            tensileStrength: 0,
            shearStrength: 0,
        },
        touch: {
            solidity: ingredient.touch.solidity * 0.8,
            stickiness: ingredient.touch.stickiness * 0.8,
            smoothness: ingredient.touch.smoothness * 0.8,
            cripinesss: ingredient.touch.cripiness,
            temperature: ingredient.touch.temperature,
            moisture: temperature.touch.moisture * 1.001,
        }
    }
};
const frying: MappingFn;
const baking: MappingFn;
const mixing: ReducingFn = (inA, inB)  =>  {
    return {
        taste: zip(inA, inB) -> map(([atomA, atomB]) => atomA + atomB),
        smell: zip(inA, inB) -> map(([atomA, atomB]) => atomA + atomB),
        physics: {
            weight: inA.physics.weight + inB.physics.weight,
            volume: inA.physics.volume + inB.physics.volume,
            tensileStrength: specialFunction(inA,  inB), // usually takes the minimum, but will have special cases e.g. flour + water
            shearStrength: anotherSpecialFunction(inA, inB), // same idea
        },
        touch: zip(inA, inB) ->  map(([aA, aB]) => average(aA, aB))
    }
}
const entangle: ReducingFn;
```
