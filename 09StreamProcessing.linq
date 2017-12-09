<Query Kind="Program" />

void Main()
{
	// part one
	foreach (var example in PartOneExamples)
	{
		var output = StreamProcessingPartOne(example.input);
		if (output == example.output)
		{
			Console.WriteLine($"SUCCESS \"{example.input}\" = {output}");
		}
		else
		{
			Console.WriteLine($"FAILURE \"{example.input}\" = {output}, but should be {example.output}");
		}
	}
	StreamProcessingPartOne(Input).Dump();

	// part two
	foreach (var example in PartTwoExamples)
	{
		var output = StreamProcessingPartTwo(example.input);
		if (output == example.output)
		{
			Console.WriteLine($"SUCCESS \"{example.input}\" = {output}");
		}
		else
		{
			Console.WriteLine($"FAILURE \"{example.input}\" = {output}, but should be {example.output}");
		}
	}
	StreamProcessingPartTwo(Input).Dump();
}

int StreamProcessingPartOne(string input)
{
	return input.ToCharArray().Aggregate((groupScore: 0, level: 0, ignore: false, garbage: false), (acc, c) => 
	(
		acc.groupScore + (!acc.ignore && !acc.garbage && c == '{' ? acc.level + 1 : 0),
		acc.level - (!acc.garbage && (c == '{' || c == '}') ? c - 124 : 0),
		IsIgnored(c, acc.ignore),
		IsGarbage(c, acc.ignore, acc.garbage)
	)).groupScore;
}

int StreamProcessingPartTwo(string input)
{
	return input.ToCharArray().Aggregate((garbageCharacters: 0, ignore: false, garbage: false), (acc, c) =>
	(
		acc.garbageCharacters + (acc.garbage && !acc.ignore && (c != '!' && c != '>') ? 1 : 0),
		IsIgnored(c, acc.ignore),
		IsGarbage(c, acc.ignore, acc.garbage)
	)).garbageCharacters;
}

bool IsIgnored(char c, bool ignore) => !ignore && c == '!';
bool IsGarbage(char c, bool ignore, bool garbage) => (garbage && (c != '>' || ignore)) || (!garbage && c == '<');

(string input, int output)[] PartOneExamples => new[] 
{
	("{}", 1),
	("{{{}}}", 6),
	("{{},{}}", 5),
	("{{{},{},{{}}}}", 16),
	("{<a>,<a>,<a>,<a>}", 1),
	("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9),
	("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9),
	("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3),
};

(string input, int output)[] PartTwoExamples => new[]
{
	("<>", 0),
	("<random characters>", 17),
	("<<<<>", 3),
	("<{!>}>", 2),
	("<!!>", 0),
	("<!!!>>", 0),
	("<{o\"i!a,<{i<a>", 10),
};

string Input => "{{{{{<!>},<'!>oi<{!!uiu\"!ao{,'!!!!o,>,<>},{<{i>},{{},{{<e,\"!!!!!>}!!!!o!!!>!}!>,<!e!!!>!!!>}!>,<<!!'!>,<!!,>},{<,>}}}},{{<{<!!{!><!>,<\",>,<!{!!!>i,u!>,<!!!>,<,a'>},{<{>,{{<!>},<!>},<i!!!{u!>},<e{!!!>}i,'!e!!!>},<!!!>,<>}}},{{<}<!!!>!}o<'{uii{a!!!!!>'u!{!!!!<>}}},{{{<!>,<\"'uo\"a!!}>}}},{{{<ueu}!>\"!!!,>},<{{>},{{}},{}}},{},{{{{<!!>,{<u\"!>},<!!!>!!{>}},<!',!u{!>o>},{{{<u!>,<!!!!<i!!!>!!!>i!>},<u!!{!>!!!>,<{\">}}}},{{},{{<!>,<}i!>\"!!!!{}>},{<,!!!>!<!!!>>}}}},{{{{{{{{<{!!'!>},<!o{!i!!'o,>},{{{{<!!!>'!>},<<!\">,{<!!o>}}},{<a!!!!,!>,<!!!!}e,o,!>},<{!!!>u'>,{<!>,<}!!}!>,<'!>,<{!!!>},<!!!e!!,{i\">}}}}},{}},{<>}},{<''!>!>!!!>'i!!!>'!>}u!>},<i!>,<!!!>,>,{<!!!>,'!>,<<!!'o!!!>ao,!!!>!>!!aao>}},{{<!>!>,<!>},<,o!!!>},<e\">},<i!>},<e,!!!>,<}'\"!>},<!!!>}!e!!>}},{<,\"ea>,<!>},<a{>},{<\"!!!>eu!{a,>,{<{!!e<'!!e!!!!a>}}},{{{<>}},{{{}},<!!ia!!<!>,<!!!!,!>,<'{!!>}},{{<!!!>,!!!><ou!,,!<,,e{!!ue>,{{}}}},{{{<\"!!\"!>i!>!!}!!!>\"!>},<!!!>'<ioo!!>}},{{<!!!>},<<e!>},<'<!>,<!>},<o>},<}\",o!!!>,<!>!eu!>!>},<!>},<!!!>e},oii>},{{<a'!!!>!>!<uo!!\"\">},{<u''eo!o<\"!>a,>,{<!,ua!>!!!>!>},<ao>}}}}},{{{},<}!>a\"'<aeo!!!!!!<!>},<i!>},<>}},{{},{{<a{'''!>,<!o!!u,e!!!>!!!>!>},<>,{}},{{<\"!>},<a!!i!!u\"!>,<{>}}}},{{{{{{<!>,<'i!!,ou,oo!a}'!!!a!!{aaui>}},{}},{}},{{{<!!o<!,!>,<!\"!>},<!!{,>}}},{{{<\"!!a\"a!>,<,!>},<!!!>>,{<!!!>,<!a{!>},<>}},{<}a,!}!>},<!>\"!o!!!i'!e!!!>a<!a!!e>},{{<{,u!!<u\"<a{\"}u!>},<,!>},<<!>},<!>},<>},<}u!!<i!!!>,<e!>,<!!!>!>!>,<ao!!!a!>,<\"!!!<>}},{<<a'!!!><>,<}>},{{<'!!e,!>,<!>!>},<e,oo!!!!!>!!!>!!!>\"!<o!>},<i>,{{}}},<u\">}},{{{<!!iu!!a>}},{{{{},{<!!!>!!!>},<>}}},{{{{{<}!!!>!!!>>}},<e!>},<!>},<<o{!>},<<{<a{'>},<,!!!e{<!>,<\"\"!}!<>}},{{}}},{}}},{{{<{<!>},<}>}},{},{{<>,{{{{<<'i!!!>'\"!>{<>}}}}}}},{{{{<!!!>{!!\"ui}!>,<{a'{!!!>!<!!!>i{!!}o>},<!>},<{\"!{!>,<\"!!u,u}!!!>,<!>,<!}!!}>},{<\"\"!!!>{uu!!!!!>}!>},<!!<ei>,{<\"!>},<i>}},{{<}!!!>u\"'!!>}}},{{<!!!>},<!aa!>},<a>,{<!!{!!a>}},{}},{{<'a!!u{!!!!,!!<>,<!!!!ii}'i!>},<,>},{{<u!>!!}!!o!>},<e{!>a<e>,{{}}},{{<!!!>!>,!!!>!!!>},<!'!>,<o<\"<!!!>!>,<>},{{},<!!<!>},<i{{ii!!!>!>,<<<>}}},{{},{{<!!!>},<>}},{{<a}\"!!u!>,<!i},a{{<\"{{\">},{{{<!>},<<!!}e!!!>,!!!>a{>}}},{{{{<a,!>},<!>,<!!,e!!'{!ui'>},{{<!>},<!>'<<u\"<!!!>!>!!!>!!>}}},{{<u!!}'ai!!}!!!>!>,<!>},<u!>,'>}},{<<''!e,!!!>},<iu<!>,<!>,<>}},{{<{!i!!<}euu!!i<!!!>!!i'!!}!>},<a!o!!!>},<>}},{{<aeuuia!>,<!>},<ia{,o!>,<!!!!!>>}}}}}}},{{{{},{{<e!>},<\"<u{e!!!>,!>},<a>,<!>},<>},{{},{<!!o,a!>},<!!!>,<{>}}},{{{{{{{{{{<!><e!!!!!>!!ee!!!!o<!>io!!!!!>!>>},{<}>}}},{<u!>,<o}!!oa!>!>},<!}}!!}!!!>,!!!>e!>!ee>,{<'!!e{o'',\"{e\"!>},<>}}}},{{},{{<'!a!>o<i!>},<!o<!!!>{>}}},{{<!>!u!>!!!>o!>,<u!>,<,e>},{<}i{u{!!{!>},<a!!e!>},<!!!u>}}},{{<!>!>,<>,<}!oe!>,<!>,<uuea!\"!!a>},{{<u!>'<!>,<}!>},<>},{<a!\"'{!>,<!!},!'!>u!!a,!!>,{}}}},{{{<uo\"!!!!'e{i>},{}}}},{{{<}!oo!!!>!!!>{!!!>'>},{{<!>},<u!!!>,<!>,<!!>},{{},<a}\"!!!>>}}},{{<<!>},<!>,<o!a<'!><!!!!,!!!>!>u\"!>>,{<'{i>}},{<e!>,<\"{!>},<!!!{\"<!>,<>},{<!!!>,<!!\"!!!><,eoi,ue{!!!!}}!!,u!!!>}>,<!!ia<!!!>{!>},<!>,<'}>}},{}},{{{{},{<{!>,<!!!!oua!>},<,!>},<o<<i!!\"{>,<}a<ui,o>},{}},{{{<!!e{{!!e>},<!!!!!!!}u\"'u\">},{<a>}},{}},{{<!!!>!<<,!>!!\"{,{!!!i}!>,<{>,{}},{{{{<!!!>},<ue!!<!>!>,<!!!>,<!!!>!!!\"{!>,<e!>o!!!>,o>},{<<\"\"iu>}}},{{<!uo!>},<!>},<o!!!>'>},{{{<}!!,!!!><o!>,\"o,>}},<!!!>{{e!!!>>},{{<!>},<o>,<e}!>au!!!!'!>},<\"!>'!>},<\"!>\"{!!!>},<!>},<{>},{{<<ai!!},!!!>uu!>,<\"!ou>},{{{<}!!ie''}!!u!>},<!!!!!',a<'\"!>,<\">}}}}}}},{}},{{{<!>!!u!!oa!>},<{!!!>o!>oa!!!>!!u!!!>'!'>,<eu}\"'i!<!!o{>}},{<!!o>,<!'!!!!i>},{{},{<ee!!!>,<\"e>}}},{{{},{<e}u},{!>!!ie{!>},<!!!>a!,'>}}}}},{{},{{<!!\"ui!!e{!!!!!>}!>,<!>},<!!!!}e,!>>}}},{{{<!>},<!!!!,!\"oia!!!ia}!!'uu>},{<!>,\"!}!,!>,<e{!>},<!!!!oi{\"a>}}}},{},{{<!!!>\"\"i!!o{u!>a!>!!'!!{{>}}},{{<!>!!!>!!,e}!>!!\"'o!!!>,<}{!u!!!>!i>,<,e<\"!>,<!!!<!>e!!{{a'>},{{{<!!'!>,<!{!>'!!a}>}},{<o,}!>eaa!>},<!iua>}},{{<!>,}<uoa!!e!!!'!!!>!'',ei!!{>}}}}},{{{{<!!!!{!\"}'o!>},<!!o{!!!>>},<!>},<!!ue}!>},<}<e!!o!>},<!!\">}},{<!>},<>,<!!i!!aa!>,<!u!>},<>},{{<\"oa'a!}!!!i!!',!>},<o!>},<!>e!>},<>}}},{{},{{<!!i'!>,<!!u}ei!!o'!!a>},{}}},{{{}},{{<!!i!!!>{,}<,a!!a!!!>!>{!o>},<!!!>{o>}}}}}},{{{{{<!!}!>,<!!!>},<{a{{u!>!>!>},<!!e!>,<u>},{{{}}}}},{{{<<e>,<\"\"!>},<!>,<!>e>},{<>}},{<!!,ea!!!!<!!!!>},{{<i!!!>u\"<ueoi!>},<,e}\"\"!>},<a>},<>}}},{{{{{<!<!!o\"!>>}}},{},{<o!>,<}',!>,<a!!{!!!>!!!i!>},<,i<!e>,<}e!>,<!!!>!!!!o!>e!u>}},{{{<u'{!!!>oo!>},<!!!'!!u>,<iee!>!ia!!o'>},{<{!i'!>,<aa'{i\"o!!i!'ee>},{<<!!!>,<!!uo!!!!iia}!!!>,u!!o{!>,<!{!\">,<!!'!>!!!u}{i'ou!>},<!ioia!>},<{!>>}},{{<}i!>},<!!!>!!,{!>},<,o!!!>,<,!!e<!>,<!!!>,u>,<>},{},{{{<!!!>\"}{>},{{<!>,<i}a\"ii!!!>!>u}u{\"{!!i>},<u,!\"'!!!>!>a!\"'u>}},{{<\"}'oa!!{!>!!<'i}a\"!'!>,!o'!!a>,<,!u!>,<a!>,<\"!{!!!>!>},<{!!a\"o>}}}}},{{{<o'!!u!!i!!\"e}a}\"!>},<!!!!,{>}},{},{<!>!!!,u!!e!!!>!>!>},<!!!>>,{<!>},<'!>!>},<<!>,<>}}}},{},{{{{{{<!!o''!>},<,,!!!!e!>,<>}},{{<<!><!>!!!>!e!!!!{,\"eae!,{iiii>,<u<>},{{<!!e>},<>},{{<,{!ou{ei>}}},{{<i>},{<a!>,<!>,<!!o<!>},<u<e!>,<>}}},{{{<a\"<!!!!e<'o!>,<u!!!>!>},<!>u'!!<!!!>>},{{},{<}<,!!!>u<!>},<}!>!,>,<<!!!!{!>},<!>,<!i{<!!!!!>},<>}}},{{<ea!!!>!}!!<!!e!!o,!>'>}}}}},{{{<i\"oo<oee>},{{},<u}!!!>!>a!>,<!>,<}<!!!o'!!a\"!>},<,,>}},{{{<!>},<o,{!'ae!>},<\"\"!!!>}>},{<\",!!a<!>!!,!>a>}},{},{<i!!\"}!>!>>,<\"'!>!>},<,\">}},{{{}},{<<,o!>,<>,<!o!!!>!>,<>},{}},{{{{<,!!!>,!i!i,!o>},{<!{!'}a!!!>!e!>!!,e\"'!>},<\"{!><e>,<!>{!!!>i{!!\"!ae<\"{}!>,<\"!>!>},<>}},{{<!!!>i!>i!!!'}!>,<\">},{<!>},<!!!!!><,'ui<!!!>!!!!!>'!'!!e!o\"o>}}},{{<\"!>,<u{oa!>,<<!>>}}}}}},{{{{<!>!{,>,{<a!>},<{<{!>,<'!>},<i!>>}}}},{{{<!!o<'!,<>,<!!!>a>}},{{<!>},<oa}eu}ee,!!,\"}!'!>,<!!ia>,{<}}a{i!>!!}!!,<!!!>,<>}},{{{}},{<}!>,ea!u},\"{!!!>i!>,<!>},<!>},<!,}>}},{<!!!!!>u!>},<},}\"e!>,<!!!>,<<!!ie}!!!>},<>,{}}},{{{},{{<!!!>o\"}!!,!>eu'!!<!>,<!>},<}!!!>!>},<!>,<a>},{<!!!!!>u<,!u!i!>!>!!!!!i!o>}}},{<>},{}},{{{{{<!>!!!>!!u{\"!!!>,<>},<,<'!ua!!<!!!>\"!!u!>,<!e}u!>},<>},{<!>,<!i!!>}}},{{{{<!!!><!!i>},{{<{ia,<!!i!a<,e<i!!o\"o!!!!<!!u!>a>,{<a!>,<}{o!!ie!>},<i'e<!>,<o!>,<!!}}<>}},{<,!!!>ee!!!>e{}},u!\"e>}}}},{{{<{e'u'>},<!!{!>,<<!!!>e!>oaa\"!>},<}!!!>,<\">},{{{<u!!!>!>},<!>,u>,<!>,<!!{!!\"<!>,<}}!!'>}},<o}}i'!!\"!>!>a!!!!!!!><>}},{{{<}!!!>,<!>!>,<!!!>!>,u{<o>}},{<e!'!>,<,!>,<e!>,<{\"!>,<<'!!o>}}}}},{{{{},{<!!!>>}},{{}},{{{{<!!!>o!!{!>,<a,e,!!!>,<i>,{<a\"i!!!!,<!>,<!!e>}},{<!!!>},<}!!\"<,!!!>},<e{ei!!'!>,<!>,<\"!!!!\"io!>,<>}},{{<i!>!!!>{!!<!!u''}oia!>,<>}}},{{},{}},{<'a!>},<!!!!!>!'!>'au!'{>,<{<e!>e!!!>}!>,,>}}},{{<o!,a<{,!!!>!!,'}'i!>},<{<'!u>,{{<}<'!!!>!i!!io,,!>},<\"!>},<>}}},{<e!>o!!o<!>},<{>,{<!!!!!>!>},<!o<!>,<!!>}}},{{{<!>,<<}!!\">,<\"!{uo!>!!!!{,!>},<eu''!!!>,>},{},{<<e!!o}>,<e<u!>,<u\"!!i>}},{{{},{{{}},{<a\"!!i!>,<!>i>,{{}}}}},{{{<!!!!!>!!!>,<!!{!!u!>!>\"<'u>},{{}}},{{<u!!!>!!!>\"!!!>>}}},{{},{<}!<ui!>,<<'>,{}},{{},{{},<'{>}}},{}},{{<>},{{<a,!>},<,,e{a!>},<!!!>u!!!!!!!>'i{<,}>},{}},{{<'!!!<aui<e!>,<u!!!{!>,<<}!>,<>},<!>o<!!i!>!!!!>}},{{<<!!ui!!,!a\"!!!!o\">}}}}},{{{{{},{{{<!{>}},{{{<'\"eo!>o!>},<!ee!!!!!!,<o!!<!>},<!>,<u!!,>}},<ii!!!>!!a}>},{<!>,<!>},<a<\"!!!!aeua!e,!!>,{<,a'}{\"\"i!!}u}!!!!!>!!!!'!!{>,{<!!!!\"!>},<!!!>aau>}}}},{{},{{<a!!!>!>},<e\"e{!i!>>},<!>,<!>i!!!>u}o!!!>!,!>},<>},{<!!!>,!>},<!>},<'!>!,!!,{'!>,<!!!>,<!>!!!!!>,<e>}}},{{{{{}},{{<'o!!!>o!!!>a,e>},{{{{<o!>,<>,<{{>},<<!>,<!!!>!>!>,<}>}}},{<\"u!>},<o!>,<,!>},<o!!!!!>,<!>},<!}'!!,o!>!<!!}>}},{{{{<'u,!>}}e!!!>!!'>},{<,>},{<!>,<}{!>,<!!!>,<o!'!!i,!!!>!!!!>,{<uu{!!!>!<!!!>!!!>\"{!!e\"'<u<o}>}}},{{<e!>},<iue{i>,{<,ua\">}},{{{{<ae\"!!!>},<,!!!<!>o>}}}},{{<oe!!<>}}},{{<o{>},{{{{<ai!>,<!!!>},<'}!!!>!>u!!!>'>},<!>,<\"!>,<{!!!>!>},<{!>,<>},<\"\"<e!>},<!i!>},<eo>}},{{<!>},<!!!>},<!!!>o!>},<!>,<!!\"!>>},{{{{<a},!>oa{\"a,>}}},{{<'!!!>,!>},<!!!>},<o\"{!>},<,,uaee!a\">},<>}}}}},{{{<\"<\"u,i>,<!!e{!!,!!}<>},{<i!>},<<!!!\"!!!!!!o!!,i!!!>ia!>},<{!!,>}},{{{<}{!!''!ea<}{!!!>\"!!!!!>!>},<!!ao>,{<<ii!>,<\"\"!>!!!!!>},<!!e\"e{\"!>,<>}},{{<!>},<!a}!!!>i{!>},<a!!!,>},{{<}!>},<!\"<<!!!>>},{{}}}},{{},{<!!!!\"a<\"\"'{!!!>!>!!>,<!ei'ea!>,!>},<<ai>}}},{{<}e!>!!\"o!>},<\"'oe>},<}!>},<<}!!!>\"''>},{{}}}},{{{<'!!a{!!!><!>},<>,{<!!,!>e,!>!u{i!>,<>}}},{{<!>},<'!>},<>}},{}},{{{},{<'!!}}<o!!,{,!>},<!!!>!!o'>}},{}}},{{<!!{>},{{{<e,u!>},<,,o!>,<!!ee!!!!ai!>,<!>},<\">}},<e!>,<<,!>,<!>{ia!a!}!!!>o!!e!!!>!!{!e>},{{<!!!!<>}}}},{{{<{>,{<\"!!<!}}!!{\"!!!<i!>},<u!!{>}}},{{}},{<,!>,<!{>}},{{<>},{{},{{{<oo!!!!'<<}a<!,!>,<!a!!!>,<<>},{<e!>,<e!!!!!>!!!>\"!!'!!!>i}>,<!!'!!!!!<'!!u!>},<!>},<!},!a{!>},<o>}},{{<!>},<}e}>,{}}},{{{{{<>},<i!>io,!ui}e!!}ii>}},{{{{}},{<'!!}a>}}}},{},{{{<!>,<e!>,<!>uu!!,!>},<>},<!!!<!!!!!>{<!'!!o!\"ooiu!'i}!!!>!>,<uu>}}}}},{<i!!!>,<!o>}},{{{<,!{{e!>},<!!!!{,<}{<!!!>\"ai<'o>},{}},{},{{<',!>},<!>},<!>!>},<!>,<i!!!!ieo!>>,<oo}{!!!>o!!!>a!>,<'!>!u,!}}{!!!'!>!!!>>}}}},{{{},{{},{{{{<!!>},{{}}}},{{<!>,<{!>}<<}!!!!}!>>},{}}}},{{{<!>,<<>},<u{}!>,<',{!u,!><\"!>},<i'{>},{{},{<>}},{{},<!>,<'!>!!<!{!>!>'{!>{{>}}},{{{<o}\"i<!!!>!>,<!!!!'!!a!>},<!>!>},<o!!\">},{{{<i!>},<!,!!'!!!>,<u,i<i<\"!>,<<u!>,<}>}}}},{{},<!!!>}!>},<a!>},<i,}!{!!o>},{{<!!!!!>!!!>!'{!!!!u>},{<}e!>,<!!{>}}},{{{<!!!>},<!oi!>},<!>,<>},{{<!!u!uo!!!>!!i{!>e'i!>},<'>,{}}},{{{}},{<!\">}}},{},{{<u!>!u,!!!>,<}aee!>,<}e!!!>'!!>},{{},{<i!!\"i!!{a!\"ua!!\"}o!!!><ao>}}}},{{{<\"a'!>},<'a!!ie,i\">},{}},{<>}}},{{{{},{{}}},{{{{<!!!!!o!>,<!!!>,<e!>},<!!{!!!!!>!!!>},<u!\"!o\"a,>},<!>,<!>,<!!!>,<<!>!!!>e>},{{{{<}'\"{}'!!\"'!!!>u>},<a'!!<!>,<,!>,<u!>},<o!!uo{!a!>>}}},{{{{<!>},<,!>,<!!!>{!>uaua!!<!!e>},{{}}},{{<!!'>},{<!>,<<!!,!!!>!o!!>,{<e}eo>}}},{{{<!!!>{>}},{<ao>,<!!ue!>},<}o\"!>,<>},{<,ea'!>},<!>,<>,{<i!!u!>},<!>,<!!!!!>o!>,<a!>},<u!>e!>u!!!>!a!!!>>,{<'!!o!!<\"!!{o!>!!!>!>},<i!!'!<<!>a}i>}}}}},{{{<!'\"iou!>},<!!>}}}}},{{{},<>},{<''>}}}},{{{{<!}{i,!!<\"!!u!<!<!!}!>,<>,<oo'o!!!!!>!!'!!o!>},<!>},<'>}},{{<'!uua!>!>,<,ou!>},<>,<i!!!><!>},<!>},<a\"!>,<!!!>,<!!>}}},{{{<ie!!!!a>},<\"i<!'>},{{}},{{<>}}},{{{}}},{{{}},{{{<}<!!!>,<!!!>i!>,<<!!!>>},{<!>,<o!!o!!!>,<{!!ou''i!!!>\"<<!!<<{o!>,<>}},{{<!!a,>},<,uu>}},{{{}},{<!!!>!!o!!!>}\"i!!<{o!!!>!i!>},<i>}}}},{{{{{}},<!<\"!>ua!>,<e!>aa!!!!e}u!!!>,<i>},{{}}},{{},{{<!>,<!!!!!>,!o!!eu!>,<{!!o,,!!!>>},{{}}},{}},{{{{<'!>,<e'e',!>},<<!>,<aa<>}},{{},{{<e{>},{<!,ie!!!>!>},<!>,<!!,!,!>!'o>}}}},{{{<e!>!!!>!uu!!!>u!>,<o>}},{{},{<!!>,<!>!>},<!>},<!>},<iia<a!!>}}},{{{<!!!!!>,<i!!<!>''>},<u!!!>},<!o}!>,<\"\">},{{},{<<aua!!e!!!!!>!>a!!!!{!!!>e,!!!>'i>}},{<}a\"o<!!!>>}},{{{<<!>,<>},<!>,<<!!!>},<!>,<}!!!>,<'>}}}},{{{{}},{{<!!!><!!!!!>u!}!!u!!i>},{{}},{{{{},<ou!!'i!>!>},<oe'!>},<\"!ou!!'o>},{<!!!!!>>,{{<!!!>,<!i<,!>},<{{!}u!!!><!!!>i!!!>'!>'>},<i!!!>!ae!>>}},{{}}}}}},{{<',>},{<!!!>}!!ae!!!u,<e>,<<o'e!!uao'\"<>},{<a{!!oe!!!>!u\"!!!>!!a!>},<>,<!!i!>},<'i,!!}!>},<!u!!!>,<!!!>!>,<u!>},<>}},{{{},<!!<,>}}},{{{{<uo<>}},{{<<!!oeieu'<!!!>>}},{<!ae{!!!>,,iuioi!i<u!!!>!!!>i!>>,{}}},{{{<ua<!>,<\">},<!!!>e!>,<,{\"i<!!!>!!!>!>}{}\"!!e>},{{},{}},{{<!!!>a!>!>,<!a!>},<eu!!i!u!e!i,e!>},<!>e!,a>,{{<!>},<,!!!>!>,!!{io!>!>},<!a'>}}},{{{},<{{{<\"{!!!>{{,a!!!>>},{{{<,e!>},<>},<!!!>!>,<!><!>!}!!!>!!\"!!!>!>},<!!!!!!i!>!!!>}!>},<!{>},<>}}}},{{{{{{{<!>,<u!>,<o!>o!!!>!\",!>eu<ue\"'a>},{{},{<!!!!!>,!!!>e,,}o'!!!>!!{!!>}},{<!!<!a!>!!!}ui!!!>},<i!a\"ou!!!>,<>}},{<{!>!!!!!>},<>,<i!!!>\">},{}},{{<o,{!!<\"u!>!>i{!>,<{!!!!!>{!>!!!>},<>,{}},{<>}},{{{<'>},{<!!eu!>},<uoo!>!!!!!>!>!!!>i>}},{{<!>!>,<!o!i{!>,<}>,{{<!!,o,!!!>>}}}},{<'>,{<e<!'!u>}}}},{{<!>u!!!><!>,<!>i}>}}}},{{},{{}},{{<'i!e!!!!!>!>!!!>{!>,<i!!!>io!>u>},<!!!!!>u,a!>},<!>}!!!>!!o!>,<!><}>}},{{}}}}},{{{{<e<!>,<i\"ie>,<i!>,<,!!a!>,<,!>},<!!e!>{u!>!>,<{>},<,i!,u!!!>u>}}}},{{},{{{<!>,<a<!!\"}'!>,<>}},{},{{<!>!>},<!!!>,e>},<!!!>},<ee>}}}},{{{<i\"\"!!{!!!>a<ea!,!!!>,<>,{<!!u!!ei!><<!'!>,<!{!!!ao<<!>!>,<a!<>}},{{{<oo!>,<>}}}},{{{{}}}},{{{<>}},{{}},{<!>},<!>,<a>,{{}}}},{{{{<u!>,<>,{<o}},!>,<o!>,<<!>},<iu>}},{}},{<!!!!!!!>e!>},<!>},<>}},{<>,{{<!!<o!!!>!,{,\"i!!}!>!>{>}}},{<,!!!>},<{',e!>,<o\"!>,<<}u!!>,{<>}}}},{{{{<!!!>!!!>!a!>!!!>!>>,<}a\"u'!!!!i!!!>},<i>},{},{{{<a\"o!!,!!!>a!!!!ue!{o!<i!>,<e,uoe>}},{{},<!!'!>,<a!!{{'{e!!,ea!>,<i!!!>,ia>},{{<<!>},<{>},{}}}}},{{},{{{{{{<o'a!!!!!>!,'!>,<'!>},<!>!a!!!>e>}}}}},{<'\"!{\"}!!!>{}'!!!!!!!!,!ie>,<!>,<!>},<!!!!o{>}},{<{>,<{!\"{!!!>>}},{{{<!>},<ioo!!!>},<!!!>},<>}},{{{},{<i!!u\"!>},<!>},<>,{<}!>},<!ei!>},<ae'!>},<!>},<!!!,>}}}}}}},{{{<'u!>!>},<,!!u!!!!,!>},<!>},<,!>},<!!{!i\"e>,{{<e!!}!>,<!!'!!<>},<!}!!!>!!ou!!!!\">}}},{{{{},{<ie'\"'}{!>,<a}}}a\"'\"'!!!>>}},{{{<!!!>!!\"eeu!>!>},<!!\"\"<\"'i>},{{{{<a!!!>!}\"{!>,<o\"e\"!>},<>},{}},{<,!!!>},a!>,<!>,<o!>o}!>},<<{a{e>}},{<e,!!!>},<<!!!!!,!>e!,!!a!\">,{<{!>},<\"!>!o,a>}}}},{}},{<!!!'\"e<>}},{{<o{,!!!>},<!!!!!>,{u!!!>a!>,<>,{}},{{},{}}}},{{},{<,,!>!>a,!!!>i!!iu!!!!!!!!!!!!!>\">}},{{}}}},{{{{{},{},{{<\"!>{o!\"!,'!>},<ui}!>,<,>}}}},{{<!>},<',{!>i!!'!!!>i>}}},{{{{{<!>},<\"!>!>!!!>,<!>},<>},{{<ui!<}!!!>uu{{<!{u!>,<!>},<!u!>!>,<>}},{{<e{,o\"{o!!!>e!!!>}!!}!>},<!!,,!>,<o!!u>}}},{{{{<!>},<!>},<{!!\"!>i!!'!>},<i!!,!!!>io>}},<!!\"u!!!>!!i>},{<,{!!!>!!i!!!!!>!!}',\"!>},<o}!>},<'o>,<o'<!>,<e<!!!i<!'!!!!!>>},{{{<!!!!<>}},<u'!!!>!!!>},<{}ao!>>}}},{{{<!!e!!!>!>,<!>!!i>}}},{{<{},!!oo{uu!!!>,<\">},{<uo'!!>},{{<i{!>},<<!>>},{{{{{<!!!>uu!>!>{>}},{{<{>}}},{},{{{}}}},{<>,<!>,<!!!>!!!>},<!!i<<{aoi>}},{{<e!i!!!!!>!>u!!o{!!ai'>},{{}}}}},{{{{<!!!>a\">,<!u\"{<>},<!>},<oo!!!>io\"oeio!!i!!!>,<a!!a!!!>,<\"<}>}},{{{{{<!>!!!>},<!>,<!!u{i{i!!<i,!!\"\"\">}},{<a!!,,!!!!e!!e>}},{<a!!!>>},{{<!>i!>o'{'!o!>},<!!!>!>!!'!!i!}>},{<,a!>},<!>},<'!!!!!!a!!!>>}}}},{{{<a<!!!>!!,>}}},{{<!>,<u!!!>!eu}!!!>}!!!>,!!!>ao!>>},{}}}},{},{}}},{{{{{{{{<!{!!!><}'oe!a'!>!au!>},<!!'>}},{}}},{{<!i,e\"!>,<ie!>},<aa\"}e<<>}}},{{{},<',!!o!\"a!!eiai<!,},>}},{{}},{{{{<'<{!!!!!!eu!u''!>u!!!>!<>},<i\">},{{<ea>,<!>aua{!e!!!>!>},<e{!!!!\"!>,<>},{{<}o!!e!>o!\"\",e!\">,<o<!>,<<!!!!!ua!>!!!>'!>},<!>,<!!u!>},<!>}!!>},{}},{{<!!{a>}}},{{<!!!>!!a{!'}!>,<<!!!>!>,<\",!>\">,{}},{<!!!>au\"',o!!!>!!\"}!>},<oe!>},<!!}u!!!>,<o>}}}}},{{{{{<!,!u!>},<<<!>!oi,<!>,<{!!!>!!!>!!>},{<!!e}<!!i<{io!>,<<u!>,<a!!,'''!>,<>}},<i!}!'!>!!uu}!!<!>,<,e>},{{{},{}},{}},{{<<}!!!!,\"!!!!!>!!o!!i,!!o<{>}}},{{<o'oo!,}!!!u!>'!>!>,<\">},{{<}}!!!>>,<<!ie!{,u!>},<!!!>e!>>},{{<!!iu!>},<}>},<!>>}},{{{<!!!>,<!oui}}\">}},<ui>}},{{{{{},{<'o{!>},<\"!!}!!!>{>}},<!>},<!!!!!>!>},<,eo!!a,'!!!>!>},<}!!{!>,<!!,!>,<i>}},{{<'!!}!!!>!<!!!>},<>,<!!!>''!!'>},{{<e\"u!!,\"!>!>,<a\"!euu>,<!!!>\"aiuu!!,!>,<o!!<!u{!>},<'!>,<\"!>>},{<!<aai<}!!o!>,<!!a}!!!>o!!!}a>,<!!!>!!,!!!>!!!>e!!!>>}}},{<ia{>}},{{{<'''\",>,<!>,<!>,<!!!!eu<aie'i,}{!!!>!!!!!i\"!!!>!>>},{<!!'!>\"\"{!!!>},<>},{{<!!!>,>},<}!!\"!!!>!!!!!>a!!!>e}\"!!o,}'>}}}},{{{{<!>,<}!>!>},<!!}}}!'!'!>,<'}au!!!>!>,<!!!>>}}},{{<<!!!>oa',!!<'}i}<'!!!>!>,<!>},<>},{{{<!!!>!>!>},<!>}!!!>,,i<\"!!!>,!!!>{>},<!},>},{{<}i!!}'{{oaa\"\"<}i>},{<!,!>},<<<e''}!!!>>}}},{{<!!ou}>},{<!>>,{<{!!u>}},{{<e!!!>}!!!!!>!>},<!!\"a!>},<a'!>!!!>\"o!!!!!>,o>,<!a{i>}}}},{{},{<ia!>,<!>o!>},<aa,i{<{,,}}!>>},{{{<a!!!>{>},{<a>,{<'!>},<!!u{>}}},{<!>,<>,{}}}},{{{},<!'a>},{{{<}!!!>\"a!!oi!a!>},<!!!!ae<>,{{}}},{<!}<!>!!!>!!!>!aioi,!>>}},{{{<>},{}},{{<<{!!e!>!>},<a!e<>},{{{<ei<!>!!!>a>}}}}},{{},<a!!!>{o,<}a!!!>},<>}},{{<!!{{!!!>!!!!!>!>o!!!>i!>},<>}}}}},{{{{<!>!e!>},<!ioui,!>,<u}>,{}}},{{<!!!>ei!>},<,>},{<!{\"\"ioo!>,<'<,'<!>},<'!!!>a!!!!!!!>!>},<u>,<{o!>,<<}i\"e>},{{<o!!\"!\"a!>,<}!{!>!!!!u!>,<'>},{<!!!>e'!!!>},<}!!!>!!!>uuu{!!!!!>},<{,\"!,>}}}},{{<}!>!>i,!!!!!>!>>},{{},{<!!!!o!!!>\"i!>},<>}},{{{{<,!{!!!!!>},<'o}!!o>,{<{}!!a,!{>}},<{<}!!!a>},{<,'!!aou'!><{>}},{<ao'}>}}},{{{<!!a!!!>!!!>!>},<},u!!\"!>},<<e!!}\"!a!!i!!!>a>},{<i!!!>i,'!>},<u<{a!u!!!!!>},<}\">}},{{{},{<o!>!>},<!!!>,<{e{!{a!!!>,<!!!!!>{!!!>,<!!!>},<!>},<>}},{{},<'!'!>,<}a<,{'eaa>},{<uo\"'ioe<!',a>}}},{{{<{e!'e!!{!!!!a!>uu!>{>,{}}}}},{{{{{{{<!}\"<>}},{}},<!!<u<}!!!!!>,<'ii!>,<!!!>},<\"!!\"!!uo>},{<!!ua!oo}a'!!>,{<!>},<!},{!>},<'o!>,<u!!!>\"{!>},<<o>}},{<a!!!>aa!>},<o>,{<>}}},{{{{{{<!>,<>},{<}!>'u!!,!!!a!!!>>}},<!{!!\"!!,,o!!!><uo!!\"<\"aio!!!>},<>}},{<\"<i!>},<!!i>,{{{{}}}}}},{<!\"'{!>{{>,{<'!!!u}!o!{>}},{}},{{{<,!!ao>},{<!>,<!>,<!ea!>,<!>},<!!!!!>,<}'!>,<!>},<!>>}},{{<\"},,!!o!>,<{!>,<!>,<{>}}},{{},{{{<!!!>,{>},{<>},{{},<!!!>!o!!!!!!\"!!!>!>,<!!i!!!>!!o<>}},{{<!>e!!!!!>}\",!>,<\"a!!!>{u'ua>}}},{{{}},{<!>},<,,{!!!>!>},<!>},<!>,<!>!{!!ai{\"!!!>a!!e!'o>}},{{<!>}i,>,<!>!!!>ue}e!>,<{!>},<i>},{{}}}}},{{{<!>},<,>,<!>},<!!!>!e!>},<ui!!>},{{{{<!!,o!,!>},<>},<o!!!>,<!!ie!}u\"<}}ae!!!>,!!e{>},<}\"!>},<!>!!!!!>!<>},<!!!!!!<!>,<>},{}},{{{<uuao!>!>,<!>}!!!!<!!!>e<!>},<!!!!i{e!!!!!>>},{{{<{!>},<!!!>>},{{}}},{<!>i\",,>}},{<a!>},<'!>ii<!!'>,<e,i{!!!>!!{}!>}!!!e{<!!}!>},<!!!>},<\"{>}},{<}!>!!e!ue!!!><!!!>\"oa!!\"ea!!!>},<!>,<>,<o!{!!!>\"!!{\"}e!!,!{!>},<!>,<!}>}}},{},{}},{{},{{{{},{}},{{{{<!>,<i\"'{,>}},<,{o!>!>}''o>},{<!!<!>,>}},{{{<\",,a!,i!!!><!><!!!>}'!!!{\"!\"}!>,<!>},<>,<!!,!!uuoo!>i>},{<!!>}}}},{{{<\"!',!!<!>,<!!}!!!!>},<!>},<\"u\"i!!!>,<!!i!>,<\"oi!>!!e\"!>!>},<}>},{{{{<!>>},{},{{},<!!!!'u!>},<a<{'i}!!!!!{!!a!>,<>}},{<e!!!>,!>!!e}<!>},<!>,<>,{}},{{{<!!!>!!!>!>\">}},{}}},{{{}}}}},{{<!!!!!>iu!!\"!>},<}u<!>,<!>},<!!!>'<<e>},{<'>,<!i!!!>\"a!!!!o!!i>},{{<!!'!>,<'<a\"!>,<,o>},{{<!>,\">},{}}}},{{{}}}}}}}";
