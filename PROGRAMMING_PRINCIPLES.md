# Принципи програмування у проєкті Sea Battle

### 1. Encapsulation (Інкапсуляція)
Дані про стан клітин ігрового поля та параметри кораблів приховані всередині класів, а доступ до них здійснюється через публічні властивості та методи.
* [Приклад у коді: Cell.cs](https://github.com/Roman9492/seabattle/blob/41c45286f982f1d620b71b636e02678f96c3104b/sea%20%E2%80%8B%E2%80%8Bbattle/Models/Cell.cs#L1)
* [Приклад у коді: Ship.cs](./sea%20battle/Models/Ship.cs)

### 2. DRY (Don't Repeat Yourself)
Логіка генерації ігрового поля та перевірки пострілів винесена в окремий клас `GameField`, що дозволяє використовувати один і той самий код як для гравця, так і для комп'ютера без дублювання.
* [Приклад у коді: GameField.cs](./sea%20battle/Models/GameField.cs)

### 3. KISS (Keep It Simple, Stupid)
Взаємодія між View та ViewModel реалізована за допомогою простого механізму `RelayCommand`, що робить обробку натискань кнопок зрозумілою та легкою для підтримки.
* [Приклад у коді: RelayCommand.cs](./sea%20battle/Models/RelayCommand.cs)
