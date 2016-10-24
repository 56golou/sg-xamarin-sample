<h1>sgQuestion sample page</h1>

<form method="post" action="post.php">
    <p>名前: <input name="name"/>  </p>
    <p>参加: 
        <input type="radio" name="join" value="1">参加
        <input type="radio" name="join" value="2">不参加
        <input type="radio" name="join" value="3">どちらともいえない
        </p>
    <p>星座: 
        <select name="horoscope">
        <option value="">選択してください</option>
        <option value="Aries">おひつじ座</option>
        <option value="Taurus">おうし座</option>
        <option value="Gemini">ふたご座</option>
        <option value="Cancer">かに座</option>
        <option value="Leo">しし座</option>
        <option value="Virgo">おとめ座</option>
        <option value="Libra">てんびん座</option>
        <option value="Scorpio">さそり座</option>
        <option value="Saggitarius">いて座</option>
        <option value="Capricorn">やぎ座</option>
        <option value="Aquarius">みずがめ座</option>
        <option value="Pisces">うお座</option>
        </select></p>    
    <p>メモ: <input name="memo" /> </p>
    <input type="submit" />
</form>
