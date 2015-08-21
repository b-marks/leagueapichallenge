set more off
clear
cd "C:\Users\Ben\Documents\Visual Studio 2015\Projects\DataParser\DataParser\bin\Debug\CSVs"
/*
* save as dta files
local files:dir "C:\Users\Ben\Documents\Visual Studio 2015\Projects\DataParser\DataParser\bin\Debug\CSVs" files "*.csv"
foreach file in `files'{
	insheet using `file',comma clear
	loc a:subinstr local file ".csv" ""
	save "`a'",replace
}

* drop duplicates
local files:dir "C:\Users\Ben\Documents\Visual Studio 2015\Projects\DataParser\DataParser\bin\Debug\CSVs" files "*.dta"
foreach file in `files'{
	use `file', clear
	qui ds
	loc a=r(varlist)
	loc b:word count `a'
	sort `a'
	token "`a'"
	gen dup=1
	forv x=1/`b'{
		di "``x''"
		replace dup=0 if ``x''[_n]!=``x''[_n+1]
	}
	tab dup
	di _r(prompt)
	if "$prompt"=="OK" {
		drop if dup>0
	}
	else{
		stop
	}
	drop dup
	save `file',replace
}
*/
* merge dta files
clear
use matches
rename id matchid
merge 1:m matchid using teams
drop _merge
rename id teamid
merge 1:m matchid teamid using participants
drop _merge
merge m:1 championid using champnames
drop _merge
gen afterAPItemChange=patch=="5.14.0.329"
save teamparticipantmerge,replace
qui levelsof cname,loc(c)
foreach champ in `c'{
	qui reg winner after if cname=="`champ'"
	mat a=e(b)
	mat b=e(V)
	*if abs(a[1,1]/sqrt(b[1,1]))>1.96 & b[1,1]!=0 {
	*	di "`champ' win rate increased by " 100*((a[1,1]+a[1,2])/a[1,2]-1) "%"
	*	*reg winner after if cname=="`champ'"
	*}
	di "`champ'," a[1,2]*100 "," a[1,1]*100 "," (a[1,1]+a[1,2])*100 "," ((a[1,1]+a[1,2])/a[1,2]-1)*100 "," sqrt(b[1,1])*100 "," a[1,1]/sqrt(b[1,1])
	/*
	gen picked = cname=="`champ'"
	qui reg picked after
	mat a=e(b)
	mat b=e(V)
	if abs(a[1,1]/sqrt(b[1,1]))>1.96 & b[1,1]!=0 {
		di "`champ' pick rate increased by " 100*((a[1,1]+a[1,2])/a[1,2]-1) "%"
		*reg picked after"
	}
	drop picked
	*/
}
forv x=0/6{
	qui levelsof item`x',loc(a`x')
	foreach i in `a`x''{
		if `i'==0{
			continue
		}
		cap gen bought`i'=0
		label variable bought`i' bought
		qui replace bought`i'=1 if item`x'==`i'
	}
}
qui ds,has(varl *bought*)
loc v `"`r(varlist)'"'
foreach y in `v'{
	qui reg `y' after duration
	mat a=e(b)
	mat b=e(V)
	loc w:subinstr local y "bought" ""
	*if abs(a[1,1]/sqrt(b[1,1]))>1.96 & abs(a[1,2]*(a[1,1]+a[1,2]))>1E-12 & abs(a[1,1])>0.01 {
	*	di "Item `w' purchase rate increased by " 100*a[1,1] "pp or " 100*((a[1,1]+a[1,2])/a[1,2]-1) "% from " a[1,2]*100 "% to " (a[1,1]+a[1,2])*100 "%."
		*reg `y' after"
	*}
	di "`w'," a[1,1]*100 "," sqrt(b[1,1])*100 "," a[1,1]/sqrt(b[1,1])
}
clear
use matches
rename id matchid
drop if queue!="NORMAL_5x5_DRAFT"
merge 1:m matchid using bans
drop _merge
merge m:1 championid using champnames
drop _merge
gen afterAPItemChange=patch=="5.14.0.329"
save banmerge,replace
qui levelsof cname,loc(c)
foreach champ in `c'{
	gen banned = cname=="`champ'"
	qui reg banned after
	mat a=e(b)
	mat b=e(V)
	*if abs(a[1,1]/sqrt(b[1,1]))>1.96 & b[1,1]!=0 {
		*di "`champ' ban rate increased by " 100*((a[1,1]+a[1,2])/a[1,2]-1) "% from "a[1,2]*100 "% to " (a[1,1]+a[1,2])*100 "%."
		*reg banned after"
	di "`champ'," a[1,2]*100 "," a[1,1]*100 "," (a[1,1]+a[1,2])*100 "," ((a[1,1]+a[1,2])/a[1,2]-1)*100 "," sqrt(b[1,1])*100 "," a[1,1]/sqrt(b[1,1])
	*}
	drop banned
}
